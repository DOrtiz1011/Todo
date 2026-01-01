using AutoFixture;
using Microsoft.EntityFrameworkCore;

namespace Todo.Tests
{
    public abstract class TestBase
    {
        private TodoDbContext _todoDbContext;

        protected TodoDbContext TestTodoDbContext
        {
            get
            {
                if (_todoDbContext == null)
                {
                    var options = new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(databaseName: "TEST_TodoDb").Options;

                    _todoDbContext = new TodoDbContext(options);
                }

                return _todoDbContext;
            }
        }

        private Fixture _fixture;

        protected Fixture TestFixture
        {
            get
            {
                if (_fixture == null)
                {
                    _fixture = new Fixture();
                }

                return _fixture;
            }
        }

        protected void ClearDatabase()
        {
            TestTodoDbContext.TodoTasks.RemoveRange(TestTodoDbContext.TodoTasks);
            TestTodoDbContext.SaveChanges();
        }
    }
}
