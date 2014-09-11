using AxSoft.Angular.Net.Sample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AxSoft.Angular.Net.Sample.Controllers.Api
{
	public class CustomersController : ApiController
	{
		// Simulate customer repository
		private static readonly List<CustomerModel> _customers = new List<CustomerModel>();

		public IEnumerable<CustomerModel> Get()
		{
			return _customers;
		}

		public HttpResponseMessage Get(int id)
		{
			var found = _customers.FirstOrDefault(x => x.Id == id);

			if (found == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, found);
		}

		public HttpResponseMessage Post(CustomerModel model)
		{
			if (!ModelState.IsValid)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
			}

			model.Id = _customers.Max(x => (int?)x.Id).GetValueOrDefault(0) + 1;
			_customers.Add(model);

			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public HttpResponseMessage Put(CustomerModel model)
		{
			if (!ModelState.IsValid)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
			}

			var index = _customers.FindIndex(x => x.Id == model.Id);

			if (index < 0)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			_customers[index] = model;
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
	}
}