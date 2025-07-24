using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.Features.JobPostings.Commands;
using HRMS.Application.Repositories;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.Application.Features.JobPostings.Handlers
{
    //işlem yap sonucu id olarak geri döndür
    public class UpdateJobPostingCommandHandler : IRequestHandler<UpdateJobPostingCommand, int>
    {
        //repository'leri kullanabilmek için tanımlıyoruz
        private readonly IWriteRepository<JobPosting> _jobPostingWriteRepository;
        private readonly IReadRepository<ApplicationUser> _userReadRepository;

        //constructor injection ile repository'leri alıyoruz
        public UpdateJobPostingCommandHandler(IWriteRepository<JobPosting> jobPostingWriteRepository, IReadRepository<ApplicationUser> userReadRepository)
        {
            _jobPostingWriteRepository = jobPostingWriteRepository;
            _userReadRepository = userReadRepository;
        }
        //request = güncelleme verilerini alacak
        //CancelationToken = İşlemleri gerekirse yarıda kesebilmek için kullanılır
        public async Task<int> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            //yeni bir entity oluşturup eskisinin üzerine yazıyoruz
            var newJobPosting = new JobPosting
            {
                Id = request.JobPostingId,
                Title = request.Title,
                Description = request.Description,
                CompanyId = request.CompanyId,
                IsActive = request.IsActive,
                CreatedAt = request.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedById = request.UserId,
                CreatedBy = await _userReadRepository.GetSingleAsync(u => u.Id == request.UserId)
            };
            //Güncelleme ve kayıt işlemleri
            await _jobPostingWriteRepository.UpdateAsync(newJobPosting);
            await _jobPostingWriteRepository.SaveChangesAsync();

            //Güncellenen ilanın id'si
            return newJobPosting.Id;
        }
    }
}
