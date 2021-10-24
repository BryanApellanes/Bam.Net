using Bam.Net.CoreServices.ApplicationRegistration.Data;

namespace Bam.Net.Presentation
{
    public class OrganizationModel
    {
        public OrganizationModel() : this(Organization.Public)
        {
        }

        public OrganizationModel(string name)
        {
            Organization = new Organization() {Name = name};
            Name = Organization.Name;
        }

        public OrganizationModel(Organization organization)
        {
            Organization = organization;
            Name = Organization.Name;
        }
        
        public string Name { get; set; }
        
        protected internal Organization Organization { get; set; }
    }
}