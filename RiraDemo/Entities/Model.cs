using Google.Protobuf.WellKnownTypes;

namespace RiraDemo.Services
{
    public class Model
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long NationalCode { get; set; }
        public DateTime BirthDate { get; set; }

        public Model FromMyModel(MyModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            NationalCode = model.NationalCode;
            BirthDate = model.BirthDate.ToDateTime();

            return this;
        }

        public MyModel ToMyModel()
        {
            return new MyModel()
            {
                FirstName = FirstName,
                LastName = LastName,
                Id = Id,
                NationalCode = NationalCode,
                BirthDate = Timestamp.FromDateTimeOffset(BirthDate.ToUniversalTime())
            };
        }
    }
}
