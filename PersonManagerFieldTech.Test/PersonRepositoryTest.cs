using PersonManagerFieldTech.Data.Model;
using PersonManagerFieldTech.Repository;

namespace PersonManagerFieldTech.Test
{
    public class PersonRepositoryTest
    {
        private PersonRepository _personRepository;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _personRepository = new PersonRepository();
        }

        [Test]
        public void GetPersons_ShouldReturnAllPersons()
        {
            // Act
            var persons = _personRepository.GetPersons();

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(persons, Is.Not.Null);
                Assert.That(persons.Count(), Is.EqualTo(2)); // Assuming two initial persons
            });
        }

        [Test]
        public void GetPersonById_ExistingId_ShouldReturnPerson()
        {
            // Arrange
            int existingId = 1;

            // Act
            var person = _personRepository.GetPersonById(existingId);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(person, Is.Not.Null);
                Assert.That(person.Id, Is.EqualTo(existingId));
            });
        }

        [Test]
        public void GetPersonById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            int nonExistingId = 999;

            // Act
            var person = _personRepository.GetPersonById(nonExistingId);

            // Assert
            Assert.That(person, Is.Null);
        }

        [Test]
        public async Task CreatePerson_ShouldAddPersonAndAssignUniqueId()
        {
            // Arrange
            var newPerson = new Person { FirstName = "Test", LastName = "User", Age = 28 };

            // Act
            int newPersonId = _personRepository.CreatePerson(newPerson);
            var retrievedPerson = _personRepository.GetPersonById(newPersonId);
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
        public void UpdatePerson_ShouldReturnTrue()
        {
            // Arrange
            var person = _personRepository.GetPersonById(1);
            person.FirstName = "Test";

            // Act
            bool response = _personRepository.UpdatePerson(person.Id, person);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(response, Is.True);
            });
        }
        
        [Test]
        public void UpdatePerson_ShouldReturnFalse()
        {
            // Arrange
            var person = _personRepository.GetPersonById(1);
            person.FirstName = "Test";

            // Act
            bool response = _personRepository.UpdatePerson(3, person);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(response, Is.False);
            });
        }

    }
}