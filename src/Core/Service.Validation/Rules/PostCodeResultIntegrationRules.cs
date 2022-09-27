using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Integration;
using Service.Validation.Interface;

namespace Service.Validation.Rules
{
    public class PostCodeResultIntegrationRules : BaseValidation<PostCodeResultDto>, IUseCaseValidation<PostCodeResultDto>
    {
        public PostCodeResultIntegrationRules(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override void Validade(PostCodeResultDto? entry)
        {
            base.Validade(entry);

            Status_MustBeSuccess(entry);
            OnValidated();

            Latitude_IsRequired(entry);
            Longitude_IsRequired(entry);

            OnValidated();
        }

        private void Status_MustBeSuccess(PostCodeResultDto? entry)
        {
            var message = "Status must be success";
            if (entry?.Status != 200)
                this.AddError("Status", message);
        }

        private void Latitude_IsRequired(PostCodeResultDto? entry)
        {
            var message = "Latitude not informed";
            if (entry?.Result.Latitude == null)
                this.AddError("Latitude", message);
        }

        private void Longitude_IsRequired(PostCodeResultDto? entry)
        {
            var message = "Longitude not informed";
            if (entry?.Result.Longitude == null)
                this.AddError("Longitude", message);
        }
    }
}