using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Todo.Api.DTO;
using Todo.Api.Exceptions;
using Todo.Api.Models;
using Todo.Api.Repository;
using Todo.Api.Service;

namespace Todo.Tests
{
    public class TodoServiceTests : TestBase
    {
        private readonly Mock<ITodoTaskRepository> _mockTodoTaskRepository;
        private readonly Mock<IValidator<TodoTaskRequestDTO>> _mockValidator;
        private readonly TodoService _todoService;
        private readonly IMapper _mapper;

        public TodoServiceTests()
        {
            _mockTodoTaskRepository = new Mock<ITodoTaskRepository>();
            _mockValidator = new Mock<IValidator<TodoTaskRequestDTO>>();
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
            _todoService = new TodoService(_mockTodoTaskRepository.Object, _mapper, _mockValidator.Object);
        }

        [Fact]
        public async Task CreateTaskAsync_WhenValid_ShouldReturnCreatedTask()
        {
            // Arrange
            var todoTaskRequestDTO = TestFixture.Build<TodoTaskRequestDTO>()
                                                .With(x => x.priority, "Low")
                                                .With(x => x.status, "NotStarted")
                                                .Create();

            var expectedItem = new TodoTask { Id = 1, Title = todoTaskRequestDTO.title };

            _mockValidator.Setup(v => v.ValidateAsync(todoTaskRequestDTO, default)).ReturnsAsync(new ValidationResult()); // Success result

            _mockTodoTaskRepository.Setup(r => r.CreateAsync(It.IsAny<TodoTask>())).Returns(Task.CompletedTask);

            // Act
            var result = await _todoService.CreateTask(todoTaskRequestDTO);

            // Assert
            result.Should().NotBeNull();
            result.title.Should().Be(todoTaskRequestDTO.title);
            result.priority.Should().Be(todoTaskRequestDTO.priority);
            result.status.Should().Be(todoTaskRequestDTO.status);
        }

        [Fact]
        public async Task CreateTaskAsync_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange
            var todoTaskRequestDTO = TestFixture.Build<TodoTaskRequestDTO>()
                                                .With(x => x.title, string.Empty)
                                                .With(x => x.priority, "Low")
                                                .With(x => x.status, "NotStarted")
                                                .Create();

            var failures = new List<ValidationFailure> { new("Title", "Title is required") };

            _mockValidator.Setup(v => v.ValidateAsync(todoTaskRequestDTO)).ReturnsAsync(new ValidationResult(failures));

            // Act
            Func<Task> act = async () => await _todoService.CreateTask(todoTaskRequestDTO);

            // Assert
            await act.Should().ThrowAsync<TodoValidationException>();
            _mockTodoTaskRepository.Verify(r => r.CreateAsync(It.IsAny<TodoTask>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistentId = 99;
            _mockTodoTaskRepository.Setup(r => r.GetByIdAsync(nonExistentId)).ReturnsAsync((TodoTask)null!);

            // Act
            Func<Task> act = async () => await _todoService.GetTaskById(nonExistentId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"TodoTask with id {nonExistentId} was not found.");
        }

        [Fact]
        public async Task GetAllTasksAsync_WhenNoTasks_ShouldReturnEmptyCollection()
        {
            // Arrange
            _mockTodoTaskRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TodoTask>());

            // Act
            var result = await _todoService.GetAllTasks();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllTasksAsync_WhenTasksExist_ShouldReturnTasks()
        {
            // Arrange
            var todoTasks = TestFixture.CreateMany<TodoTask>(3).ToList();
            _mockTodoTaskRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(todoTasks);

            // Act
            var result = await _todoService.GetAllTasks();

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetByIdAsync_WhenFound_ShouldReturnTask()
        {
            // Arrange
            var existingId = 1;
            var todoTask = TestFixture.Build<TodoTask>()
                                      .With(x => x.Id, existingId)
                                      .Create();

            _mockTodoTaskRepository.Setup(r => r.GetByIdAsync(existingId)).ReturnsAsync(todoTask);

            // Act
            var result = await _todoService.GetTaskById(existingId);

            // Assert
            result.Should().NotBeNull();
            result.id.Should().Be(existingId);
        }

        [Fact]
        public async Task CreateTaskAsync_WhenValid_ShouldCallRepositoryCreateOnce()
        {
            // Arrange
            var todoTaskRequestDTO = TestFixture.Build<TodoTaskRequestDTO>()
                                                .With(x => x.priority, "Medium")
                                                .With(x => x.status, "InProgress")
                                                .Create();

            _mockValidator.Setup(v => v.ValidateAsync(todoTaskRequestDTO, default)).ReturnsAsync(new ValidationResult()); // Success result
            
            // Act
            await _todoService.CreateTask(todoTaskRequestDTO);
            
            // Assert
            _mockTodoTaskRepository.Verify(r => r.CreateAsync(It.IsAny<TodoTask>()), Times.Once);
        }
    }
}
