using AutoMapper;

namespace DataLayer
{
    public class TableMapper<TSource, TDestination> where TSource : class where TDestination : class
    {
        public TableMapper()
        {
            Mapper.CreateMap<DTOs.StudentDTO, Student_2>();
            Mapper.CreateMap<Student_2, DTOs.StudentDTO>();
        }
        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }
}
