using API.DTOs;
using API.Interfaces;

namespace API.Factories
{
    public class EnumValueDtoFactory : IEnumValueDtoFactory
    {
        public List<EnumValueDTO> Create<T>() where T : Enum
        {
            var enumType = typeof(T);

            var names = Enum.GetNames(enumType);

            var result = new List<EnumValueDTO>();

            foreach (var name in names)
            {
                result.Add(new EnumValueDTO()
                {
                    Name = name,
                    Value = (int)Enum.Parse(enumType, name)
                });
            }

            return result;
        }
    }
}
