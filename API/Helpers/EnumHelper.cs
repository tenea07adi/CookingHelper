using API.Models.DTOs;

namespace API.Helpers
{
    public static class EnumHelper
    {
        public static List<EnumValueDTO> GetEnumValues<T>() where T : Enum 
        {
            var enumType = typeof(T);

            var names = Enum.GetNames(enumType);

            var result = new List<EnumValueDTO>();

            foreach ( var name in names )
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
