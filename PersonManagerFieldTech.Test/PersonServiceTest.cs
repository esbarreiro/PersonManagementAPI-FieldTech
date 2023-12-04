using Moq;
using PersonManagerFieldTech.Controllers;
using PersonManagerFieldTech.Data.Model;
using PersonManagerFieldTech.Repository;
using PersonManagerFieldTech.Repository.Interfaces;
using PersonManagerFieldTech.Services.Services;
using PersonManagerFieldTech.Services.Services.Interfaces;
using PersonManagerFieldTech.Services.ViewModels;

namespace PersonManagerFieldTech.Test
{
    public class PersonServiceTest
    {
        private PersonService _personService;
        private IQueryable<Person> _expectedPersons;
        private new Mock<IPersonRepository> _personRepositoryMock;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var mockRepository = new Mock<IPersonRepository>();

            var expectedPerson = new List<Person>
            {
                new Person { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 },
                new Person { Id = 2, FirstName = "Jane", LastName = "Doe", Age = 25 },
                new Person { Id = 3, FirstName = "James", LastName = "Doe", Age = 24 },
                new Person { Id = 4, FirstName = "Jamie", LastName = "Doe", Age = 23 },
            };

            mockRepository.Setup(repo => repo.GetPersons()).Returns(expectedPerson.AsQueryable());

            _expectedPersons = expectedPerson.AsQueryable();
        }

        [Test]
        public async Task GetPersons_ShouldReturnAllPersons()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();

            _personRepositoryMock.Setup(repo => repo.GetPersons()).Returns(_expectedPersons);
            var _personService = new PersonService(_personRepositoryMock.Object);

            // Act
            var persons = await _personService.GetPersons();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(persons, Is.Not.Null);
                Assert.That(persons.Count(), Is.EqualTo(4)); // Assuming two initial persons
            });
        }

        [Test]
        public async Task GetPersonById_ExistingId_ShouldReturnPersonViewModel()
        {
            // Arrange
            int existingId = 1;

            _personRepositoryMock = new Mock<IPersonRepository>();

            _personRepositoryMock.Setup(repo => repo.GetPersonById(1)).Returns(new Person { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 });

            var _personService = new PersonService(_personRepositoryMock.Object);

            // Act
            var person = await _personService.GetPersonById(existingId);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(person, Is.Not.Null);
                Assert.That(person.Id, Is.EqualTo(existingId));
                Assert.That(person, Is.TypeOf<PersonViewModel>());
            });
        }

        [Test]
        public async Task GetPersonById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            int nonExistingId = 999;

            _personRepositoryMock = new Mock<IPersonRepository>();

            _personRepositoryMock.Setup(repo => repo.GetPersonById(nonExistingId)).Returns((Person)null);

            var _personService = new PersonService(_personRepositoryMock.Object);

            // Act
            var person = await _personService.GetPersonById(nonExistingId);

            // Assert
            Assert.That(person, Is.Null);
        }

        [Test]
        public async Task CreatePerson_ShouldAddPersonAndAssignUniqueId()
        {
            // Arrange
            var newPerson = new PersonViewModel { Id = 5, FirstName = "Test", LastName = "User", Age = 28 };

            _personRepositoryMock = new Mock<IPersonRepository>();

            _personRepositoryMock.Setup(repo => repo.CreatePerson(new Person { FirstName = "Test", LastName = "User", Age = 28 })).Returns(5);

            var _personService = new PersonService(_personRepositoryMock.Object);

            // Act
            int newPersonId = await _personService.CreatePerson(newPerson);
            var retrievedPerson = await _personService.GetPersonById(newPersonId);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(retrievedPerson.Id, Is.EqualTo(newPersonId));
                Assert.That(retrievedPerson.FirstName, Is.EqualTo(newPerson.FirstName));
                Assert.That(retrievedPerson.LastName, Is.EqualTo(newPerson.LastName));
                Assert.That(retrievedPerson.Age, Is.EqualTo(newPerson.Age));
            });
        }

        [Test]
        public async Task UpdatePerson_ShouldReturnTrue()
        {
            // Arrange

            _personRepositoryMock = new Mock<IPersonRepository>();

            _personRepositoryMock.Setup(repo => repo.GetPersonById(1)).Returns(new Person { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 });

            var _personService = new PersonService(_personRepositoryMock.Object);

            var person = await _personService.GetPersonById(1);
            person.FirstName = "Test";

            _personRepositoryMock = new Mock<IPersonRepository>();

            _personRepositoryMock.Setup(repo => repo.UpdatePerson(person.Id, new Person() { Age = person.Age, Id = person.Id, FirstName = person.FirstName, LastName = person.LastName})).Returns(true);

            _personService = new PersonService(_personRepositoryMock.Object);

            // Act
            bool response = await _personService.UpdatePerson(person.Id, person);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(response, Is.True);

            });
        }
    }
}