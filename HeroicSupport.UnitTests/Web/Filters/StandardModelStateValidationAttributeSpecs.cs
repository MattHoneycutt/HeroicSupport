using System.Web.Mvc;
using HeroicSupport.Web.Filters;
using NUnit.Framework;
using SpecsFor;
using Should;
using SpecsFor.ShouldExtensions;

namespace HeroicSupport.UnitTests.Web.Filters
{
	public class StandardModelStateValidationAttributeSpecs
	{
		public class when_applied_to_a_valid_request : SpecsFor<StandardModelStateValidationAttribute>
		{
			protected override void When()
			{
				GetMockFor<ControllerBase>().Object.ViewData = new ViewDataDictionary();
				
				GetMockFor<ActionExecutingContext>().Setup(c => c.Controller).Returns(GetMockFor<ControllerBase>().Object);
				
				SUT.OnActionExecuting(GetMockFor<ActionExecutingContext>().Object);
			}

			[Test]
			public void then_it_does_not_set_a_result()
			{
				GetMockFor<ActionExecutingContext>()
					.Object.Result.ShouldBeNull();
			}
		}

		public class when_applied_to_an_invalid_request : SpecsFor<StandardModelStateValidationAttribute>
		{
			protected override void When()
			{
				GetMockFor<ControllerBase>().Object.ViewData = new ViewDataDictionary();
				GetMockFor<ControllerBase>().Object.ViewData.ModelState.AddModelError("*", "Something");

				GetMockFor<ActionExecutingContext>().Setup(c => c.Controller).Returns(GetMockFor<ControllerBase>().Object);

				SUT.OnActionExecuting(GetMockFor<ActionExecutingContext>().Object);
			}

			[Test]
			public void then_it_returns_a_view_result()
			{
				GetMockFor<ActionExecutingContext>()
					.Object.Result.ShouldLookLike(new ViewResult());
			}
		}
	}
}