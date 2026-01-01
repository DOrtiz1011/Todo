using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Todo.APi.Models;
using Todo.APi.Repository;

namespace Todo.Tests
{
    public class TodoRepositoryTests : TestBase, IDisposable
    {
        private TodoTaskRepository _TodoTaskRepository;

        public TodoRepositoryTests()
        {
            _TodoTaskRepository = new TodoTaskRepository(TestTodoDbContext);
        }

        public void Dispose()
        {
            _TodoTaskRepository = null;
            ClearDatabase();
        }

        [Fact]
        public async Task CreateAsync_ShouldSaveTodoToDatabase()
        {
            // Arrange
            var todoTask = TestFixture.Build<TodoTask>()
                                      .With(x => x.Id, 0)
                                      .Create();

            // Act
            await _TodoTaskRepository.CreateAsync(todoTask);

            // Assert
            var savedTodoTask = await TestTodoDbContext.TodoTasks.FirstOrDefaultAsync();

            savedTodoTask.Should().NotBeNull();
            savedTodoTask.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTodo_WhenTodoExists()
        {
            // Arrange
            var todoTask = TestFixture.Build<TodoTask>()
                                      .With(x => x.Id, 0)
                                      .Create();

            await _TodoTaskRepository.CreateAsync(todoTask);

            // Act
            var retrievedTodoTask = await _TodoTaskRepository.GetByIdAsync(todoTask.Id);

            // Assert
            CompareTodoTasks(retrievedTodoTask, todoTask);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenTodoDoesNotExist()
        {
            // Act
            var retrievedTodoTask = await _TodoTaskRepository.GetByIdAsync(999);

            // Assert
            retrievedTodoTask.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingTodoInDatabase()
        {
            // Arrange
            var todoTask = TestFixture.Build<TodoTask>()
                                      .With(x => x.Id, 0)
                                      .Create();

            await _TodoTaskRepository.CreateAsync(todoTask);
            
            // Modify properties
            todoTask.Title       = "Updated Title";
            todoTask.Description = "Updated Description";
            todoTask.Status      = TodoStatus.Completed;
            
            // Act
            await _TodoTaskRepository.UpdateAsync(todoTask);
            
            // Assert
            var updatedTodoTask = await _TodoTaskRepository.GetByIdAsync(todoTask.Id);
            CompareTodoTasks(todoTask, updatedTodoTask);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveTodoFromDatabase()
        {
            // Arrange
            var todoTask = TestFixture.Build<TodoTask>()
                                      .With(x => x.Id, 0)
                                      .Create();

            await _TodoTaskRepository.CreateAsync(todoTask);
            
            // Act
            await _TodoTaskRepository.DeleteAsync(todoTask.Id);
            
            // Assert
            var deletedTodoTask = await _TodoTaskRepository.GetByIdAsync(todoTask.Id);
            deletedTodoTask.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTodos()
        {
            // Arrange
            var todoTasks = TestFixture.Build<TodoTask>()
                                       .With(x => x.Id, 0)
                                       .CreateMany(5)
                                       .ToList();

            foreach (var todoTask in todoTasks)
            {
                await _TodoTaskRepository.CreateAsync(todoTask);
            }

            // Act
            var retrievedTodoTasks = await _TodoTaskRepository.GetAllAsync();
            
            // Assert
            retrievedTodoTasks.Should().HaveCount(5);
            
            foreach (var todoTask in todoTasks)
            {
                var retrievedTodoTask = retrievedTodoTasks.FirstOrDefault(t => t.Id == todoTask.Id);
                CompareTodoTasks(todoTask, retrievedTodoTask);
            }
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoTodosExist()
        {
            // Act
            var retrievedTodoTasks = await _TodoTaskRepository.GetAllAsync();
            
            // Assert
            retrievedTodoTasks.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenTodoIsNull()
        {
            // Act
            Func<Task> act = async () => await _TodoTaskRepository.CreateAsync(null);
            
            // Assert
            await act.Should().ThrowAsync<NullReferenceException>();
        }

        private void CompareTodoTasks(TodoTask expected, TodoTask actual)
        {
            actual.Should().NotBeNull();
            actual.Id.Should().BeGreaterThan(0);
            actual.Title.Should().Be(expected.Title);
            actual.Description.Should().Be(expected.Description);
            actual.DueDateTime.Should().Be(expected.DueDateTime);
            actual.Status.Should().Be(expected.Status);
            actual.Priority.Should().Be(expected.Priority);
        }
    }
}
