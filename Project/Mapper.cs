using AutoMapper;
using Project.DTO;
using Project.Model;


namespace Project
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<DonorDto, Donor>().ReverseMap();
            CreateMap<PresentDto, Present>().ReverseMap();
            CreateMap<LotteryDto, Lottery>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartItemDto, CartItem>().ReverseMap();

        }
    }
}
