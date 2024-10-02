    using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using Vives.Services.Model;

namespace PeopleManager.Services
{
    public class OrganizationService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public OrganizationService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<OrganizationResult>> Find()
        {
            return await _dbContext.Organizations
                .Select(o => new OrganizationResult
                {
                    Name = o.Name,
                    Id = o.Id,
                    Description = o.Description,
                    NumberOfMembers = o.Members.Count
                })
                .ToListAsync();
        }

        //Get (by id)
        public async Task<OrganizationResult?> Get(int id)
        {
            return await _dbContext.Organizations
                .Select(o => new OrganizationResult
                {
                    Name = o.Name,
                    Id = o.Id,
                    Description = o.Description,
                    NumberOfMembers = o.Members.Count
                })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Create
        public async Task<ServiceResult<OrganizationResult>> Create(OrganizationRequest request)
        {
            var serviceResult = new ServiceResult<OrganizationResult>();
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                serviceResult.Messages.Add(new ServiceMessage
                {
                    Code = "NotEmpty",
                    Message = $"Name cannot ve empty",
                    Type = MessageType.Error
                });
            }

            var organization = new Organization
            {
                Name = request.Name,
                Description = request.Description,
            };

            _dbContext.Organizations.Add(organization);
            await _dbContext.SaveChangesAsync();

            var result = await Get(organization.Id);
            
            serviceResult.Data = result;
            
            if(serviceResult is null)
            {
                serviceResult.Messages.Add(new ServiceMessage
                {
                    Code = "NotFound",
                    Message = $"Could not find Organization for Id {organization.Id}",
                    Type = MessageType.Error
                });
            }
            return serviceResult;
        }

        //Update
        public async Task<OrganizationResult?> Update(int id, OrganizationRequest request)
        {
            var Organization = _dbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (Organization is null)
            {
                return null;
            }

            Organization.Name = request.Name;
            Organization.Description = request.Description;

            await _dbContext.SaveChangesAsync();

            return await Get(id);
        }

        //Delete
        public async Task Delete(int id)
        {
            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (organization is null)
            {
                return;
            }

            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync();
        }

    }
}
