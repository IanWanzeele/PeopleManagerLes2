using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class PersonService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PersonService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<Person>> Find()
        {
            return await _dbContext.People
                .Include(p => p.Organization)
                .ToListAsync();
        }

        //Get (by id)
        public async Task<Person?> Get(int id)
        {
            return await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Create
        public async Task<Person?> Create(Person person)
        {
            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();

            return person;
        }

        //Update
        public async Task<Person?> Update(int id, Person person)
        {
            var dbPerson = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.OrganizationId = person.OrganizationId;

            await _dbContext.SaveChangesAsync();

            return dbPerson;
        }

        //Delete
        public async Task Delete(int id)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return;
            }

            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();
        }

    }
}
