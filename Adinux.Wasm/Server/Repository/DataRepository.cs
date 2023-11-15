using Adinux.Wasm.Server.DTOs;
using Adinux.Wasm.Server.Models;
using Adinux.Wasm.Shared.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Adinux.Wasm.Server.Repository
{
    public class DataRepository
    {
        private readonly EFContext _context;
        public DataRepository(EFContext context)
        {
            _context = context;
        }

        public async Task<APIResponseViewModel<UserIdentityFormViewModel>> GetOrCreateUserForIdentityFormAsync(UserIdentityFormViewModel userIdentityForm)
        {
            try
            {
                var user = await _context.UserForms.Where(u => u.Email == userIdentityForm.Email && u.Mobile == userIdentityForm.Mobile.ToString()).FirstOrDefaultAsync();
                if (user != null) userIdentityForm.Id = user.Id;
                else
                {
                    // Create User
                    var newUser = new UserForm()
                    {
                        FirstName = userIdentityForm.FirstName,
                        LastName = userIdentityForm.LastName,
                        Mobile = userIdentityForm.Mobile.ToString(),
                        Email = userIdentityForm.Email,
                        Company = userIdentityForm.Company,
                    };
                    _context.UserForms.Add(newUser);
                    await _context.SaveChangesAsync();

                    userIdentityForm.Id = newUser.Id;
                }

               return GenerateSuccessResponse(userIdentityForm);
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse<UserIdentityFormViewModel>(new List<string>() { ex.ToString() });
            }
        }
        
        public async Task<APIResponseViewModel<CatalogueRequestSenderQueueDTO>> AddCataloguesRequestAsync(CataloguesRequestViewModel cataloguesRequestView)
        {
            try
            {
                var user = await _context.UserForms.FindAsync(cataloguesRequestView.UserId);
                if(user != null)
                {
                    List<CatalogueRequestSendType> catalogueRequestSendTypes = new List<CatalogueRequestSendType>();
                    foreach (var sendType in cataloguesRequestView.SendTypes)
                    {
                        catalogueRequestSendTypes.Add(new CatalogueRequestSendType
                        {
                            SendType = sendType,
                            
                        });
                    }

                    StringBuilder cataloguesString = new StringBuilder();
                    foreach (var selectedCatalogue in cataloguesRequestView.SelectedCatalogues)
                    {
                        cataloguesString.Append(selectedCatalogue.Name);
                        cataloguesString.Append(",");
                    }
                    var catalogueRequestObject = new CatalogueRequest
                    {
                        CatalogueRequestSendTypes = catalogueRequestSendTypes,
                        UserFormId = user.Id,
                        CataloguesNames = cataloguesString.ToString()

                    };

                    _context.CatalogueRequests.Add(catalogueRequestObject);

                    await _context.SaveChangesAsync();

                   return GenerateSuccessResponse(new CatalogueRequestSenderQueueDTO
                   {
                       Email = user.Email,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       Mobile = user.Mobile,
                       CatalogueRequestSendTypes = catalogueRequestObject.CatalogueRequestSendTypes,
                       SelectedCatalogues = cataloguesRequestView.SelectedCatalogues
                   });
                }
                else
                {
                    return GenerateErrorResponse<CatalogueRequestSenderQueueDTO>(new List<string>() {"User Not Found" });
                }

            }
            catch (Exception ex)
            {

                return GenerateErrorResponse<CatalogueRequestSenderQueueDTO>(new List<string>() { ex.ToString() });
            }
        }
        public async Task<APIResponseViewModel<bool>> AddRepresentationRequestAsync(RepresentationRequestViewModel representationRequestViewModel)
        {
            try
            {
                var user = await _context.UserForms.FindAsync(representationRequestViewModel.UserId);
                if(user != null)
                {
                    
                    var representationRequestObject = new RepresentationRequest
                    {
                        UserFormId = user.Id,
                        City = representationRequestViewModel.City,
                        Province = representationRequestViewModel.Province,
                        RepresentationServiceType = representationRequestViewModel.RepresentationServiceType,
                        RepresentationPersonType = representationRequestViewModel.RepresentationPersonType,
                        CreatedAt = DateTime.Now
                    };

                    _context.RepresentationRequests.Add(representationRequestObject);

                    await _context.SaveChangesAsync();

                    return GenerateSuccessResponse(true);
                }
                else
                {
                    return GenerateErrorResponse<bool>(new List<string>() {"User Not Found" });
                }

            }
            catch (Exception ex)
            {

                return GenerateErrorResponse<bool>(new List<string>() { ex.ToString() });
            }
        }
        public async Task<APIResponseViewModel<bool>> AddPriceRequestAsync(PriceRequestFormViewModel priceRequestFormViewModel)
        {
            try
            {
                var user = await _context.UserForms.FindAsync(priceRequestFormViewModel.UserId);
                if(user != null)
                {
                    
                    var priceRequestObject = new PriceRequest
                    {
                        UserFormId = user.Id,
                        ProjectAddress = priceRequestFormViewModel.ProjectAddress,
                        ProjectName = priceRequestFormViewModel.ProjectName,
                        Desc = priceRequestFormViewModel.Desc,
                        CreatedAt = DateTime.Now
                    };

                    _context.PriceRequests.Add(priceRequestObject);


                    await _context.SaveChangesAsync();

                    return GenerateSuccessResponse(true);
                }
                else
                {
                    return GenerateErrorResponse<bool>(new List<string>() {"User Not Found" });
                }

            }
            catch (Exception ex)
            {

                return GenerateErrorResponse<bool>(new List<string>() { ex.ToString() });
            }
        }

        public async Task<APIResponseViewModel<bool>> AddResumeRequestAsync(ResumeRequestViewModel resumeRequestViewModel)
        {
            try
            {
                var user = await _context.UserForms.FindAsync(resumeRequestViewModel.UserId);
                if (user != null)
                {
                    List<ResumeRequestSendType> resumeRequestSendTypes = new List<ResumeRequestSendType>();
                    foreach (var sendType in resumeRequestViewModel.SendTypes)
                    {
                        resumeRequestSendTypes.Add(new ResumeRequestSendType
                        {
                            SendType = sendType,

                        });
                    }

  
                    var resumeRequestObject = new ResumeRequest
                    {
                        ResumeRequestSendTypes = resumeRequestSendTypes,
                        UserFormId = user.Id,
                    };

                    _context.ResumeRequests.Add(resumeRequestObject);

                    await _context.SaveChangesAsync();

                    return GenerateSuccessResponse(true);
                }
                else
                {
                    return GenerateErrorResponse<bool>(new List<string>() { "User Not Found" });
                }

            }
            catch (Exception ex)
            {

                return GenerateErrorResponse<bool>(new List<string>() { ex.ToString() });
            }
        }

        public async Task UpdateCatalogueRequestSendTypeAsync(CatalogueRequestSendType catalogueRequestSendType)
        {
            _context.Update(catalogueRequestSendType);
            await _context.SaveChangesAsync();
        }

        private APIResponseViewModel GenerateErrorResponse(List<string> errors)
        {
            return new APIResponseViewModel()
            {
                IsSuccesfull = false,
                Errors = errors
            };
        }
        private APIResponseViewModel<T> GenerateErrorResponse<T>(List<string> errors)
        {
            return new APIResponseViewModel<T>()
            {
                IsSuccesfull = false,
                Errors = errors
            };
        }

        private APIResponseViewModel GenerateSuccessResponse()
        {
            return new APIResponseViewModel()
            {
                IsSuccesfull = true,
            };
        }
        private APIResponseViewModel<T> GenerateSuccessResponse<T>(T result)
        {
            return new APIResponseViewModel<T>()
            {
                IsSuccesfull = true,
                Result = result
            };
        }

    }
}
