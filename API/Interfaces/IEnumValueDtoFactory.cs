using API.DTOs;

namespace API.Interfaces
{
    public interface IEnumValueDtoFactory
    {
        public List<EnumValueDTO> Create<T>() where T : Enum;
    }
}
