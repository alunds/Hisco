namespace Hisco.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Models;
    using Repositories;
    using Security;

    public class EntriesController : ApiController
    {
        private readonly IRepository<Entry> _repository;
        private readonly ISecurity _security;
        private const string SecurityHeaderName = "X-Hisco";
        private const string GenericErrorMessage = "Invalid entry.";

        public EntriesController(IRepository<Entry> repository, ISecurity security)
        {
            _repository = repository;
            _security = security;
        }

        // GET /api/entries
        public IEnumerable<Entry> Get()
        {
            return _repository.Get();
        }

        // POST /api/entries
        public HttpResponseMessage Post(Entry value)
        {
            if (value == null || !ModelState.IsValid || !Request.Headers.Contains(SecurityHeaderName))
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GenericErrorMessage);

            string headerHash = Request.Headers.GetValues(SecurityHeaderName).First();
            string secureHash = _security.GenerateHash(new[] {value.Name, Math.Floor(value.Score).ToString(CultureInfo.InvariantCulture)});

            if (headerHash != secureHash)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GenericErrorMessage);

            _repository.Add(value);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}