using System.Text.Json.Serialization;
using UserManager.Domain.Validation;

namespace UserManager.Domain.Entities
{
    public class Member : Entity
    {
        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }

        public Member(string firstname, string lastname, string gender, string email, bool? active)
        {
            ValidateDomain(firstname, lastname, gender, email, active);
        }

        public Member() { }

        [JsonConstructor]
        public Member(int id, string fistName, string lastName, string gender, string email, bool? isActive)
        {
            DomainValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(fistName, lastName, gender, email, isActive);
        }

        public void Update(string fistName, string secondName, string gender, string email, bool? isActive)
        {
            ValidateDomain(fistName, secondName, gender, email, isActive);
        }

        private void ValidateDomain(string fistName, string lastName, string gender, string email, bool? active)
        {
            DomainValidation.When(string.IsNullOrEmpty(fistName), "Invalid name. FistName is required");

            DomainValidation.When(fistName.Length < 3, "Invalid name, too short, minumum 3 characters");

            DomainValidation.When(string.IsNullOrEmpty(lastName), "Invalid lastname. LastName is required");

            DomainValidation.When(lastName.Length < 3, "Invalid lastName, too short, minumum 3 characters");

            DomainValidation.When(email?.Length > 250, "Invalid email, too short, maximum 250 characters");

            DomainValidation.When(email?.Length < 10, "Invalid email, too short, maximum 10 characters");

            DomainValidation.When(string.IsNullOrEmpty(gender), "Invalid gender. Gender is required");

            DomainValidation.When(!active.HasValue, "Must define activity");

            FistName = fistName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            IsActive = active;

        }
    }
}
