public class MockUowBuilder
    {
        public Mock<IUnitOfWork> MockUnitOfWork {
            get { return _mockUnitOfWork; }
        }

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ReseauverContext> _mockContext = new Mock<ReseauverContext>("connectionString");

        public MockUowBuilder()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.Context).Returns(_mockContext.Object);
        }
        
        public void SetupDatabase<T>(InMemoryDbSet<T> dbSet) where T : class
        {
            _mockContext.Setup(x => x.Set<T>()).Returns(dbSet);
            var repository = new Repository<T>(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(x => x.CreateRepository<T>()).Returns(repository);
        }

    }