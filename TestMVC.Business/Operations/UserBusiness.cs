using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMVC.Business.Interfaces;
using TestMVC.Data.Interfaces;
using TestMVC.Model.Common;
using TestMVC.Model.User;

namespace TestMVC.Business.Operations
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            mapper = new Mapper(AutoMapperConfiguration.Configure());
            this.userRepository = userRepository;
        }

        public async Task<ICollection<UserViewModel>> GetAllAsync()
        {
            return mapper.Map<ICollection<UserViewModel>>(await userRepository.GetAllAsync());
        }

        public async Task<UserViewModel> GetAsync(int id)
        {
            return mapper.Map<UserViewModel>(await userRepository.GetAsync(id));
        }

        public async Task<OperationResult> SaveAsync(UserViewModel request)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (request.Password.Equals(request.ConfirmPassword))
                {
                    // Si el Id es distinto a null y la contraseña es distinta a Ma$ter12 se actualiza la contraseña
                    if(request.Id != null && !request.Password.Equals("Ma$ter12"))
                    {
                        await userRepository.UpdatePasswordAsync(Convert.ToInt32(request.Id), Common.HashStringSHA256(request.Password));
                    }

                    // Si el Id es null es usuario nuevo
                    if(request.Id == null)
                    {
                        // Se valida que no exista otro username igual
                        if(await userRepository.UsernameExistsAsync(request.Id, request.Username))
                        {
                            result.Operation = false;
                            result.Message = "El Usuario ya existe";

                            return result;
                        }

                        // Se crea el usuario
                        await userRepository.SaveAsync(request.Email, request.Username, Common.HashStringSHA256(request.Password), request.Genre[0]);

                        result.Operation = true;
                    }

                    await userRepository.UpdateUserAsync(Convert.ToInt32(request.Id), request.Username, request.Email);
                }
                else
                {
                    result.Operation = false;
                    result.Message = "El password no coincide";
                }
            }
            catch(Exception ex)
            {
                result.Operation = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> ChangeStatusAsync(int id, bool status)
        {
            OperationResult result = new OperationResult();

            try
            {
                await userRepository.ChangeStatusAsync(id, status);

                result.Operation = true;
            }
            catch (Exception ex)
            {
                result.Operation = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                await userRepository.DeleteAsync(id);

                result.Operation = true;
            }
            catch(Exception ex)
            {
                result.Operation = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
