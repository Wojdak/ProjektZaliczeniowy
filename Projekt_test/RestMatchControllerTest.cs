using Projekt_zaliczeniowy;
using Projekt_zaliczeniowy.Models;
using Microsoft.AspNetCore.Mvc;
using Projekt_zaliczeniowy.Models.Interfaces;
using Projekt_zaliczeniowy.Controllers;

namespace Projekt_test
{
    public class RestMatchControllerTest
    {
        private readonly IMatchService matchService;
        private readonly RestMatchController controller;

        public RestMatchControllerTest()
        {
            this.matchService = new MatchServiceEFTest();
            this.controller = new RestMatchController(matchService);

            matchService.Save(new Match { Id = 1, HostId = 2, GuestId = 4, Date = DateTime.Parse("2022-12-26 17:30"), Tickets_amount = 15, Price = 50 });
            matchService.Save(new Match { Id = 2, HostId = 3, GuestId = 1, Date = DateTime.Parse("2022-12-28 21:00"), Tickets_amount = 4, Price = 40 });
            matchService.Save(new Match { Id = 3, HostId = 4, GuestId = 3, Date = DateTime.Parse("2022-12-31 13:30"), Tickets_amount = 5, Price = 50 });
            matchService.Save(new Match { Id = 4, HostId = 1, GuestId = 2, Date = DateTime.Parse("2023-01-02 20:45"), Tickets_amount = 2, Price = 45 });
        }

        [Fact]
        public void GetTest()
        {
            ActionResult<IEnumerable<Match?>> result = controller.GetMatches();
            List<Match> list = new List<Match>(result.Value);
            Assert.Equal(4, list.Count);
        }
        [Fact]
        public void GetByIdTest()
        {
            ActionResult<Match> a = controller.GetMatch(1);
            ActionResult<Match> b = controller.GetMatch(15);
            Assert.IsType<Match>(a.Value);
            Assert.IsType<NotFoundResult>(b.Result);
        }

        [Fact]
        public void PostTest()
        {
            Match match = new Match{ Id = 5, HostId = 2, GuestId = 1, Date = DateTime.Parse("2023-01-15 21:30"), Tickets_amount = 8, Price = 50 };
            Match match_error = new Match { HostId = 3, GuestId = 2 };

            ActionResult<Match> a = controller.PostMatch(match);

            controller.ModelState.AddModelError("test", "test");
            ActionResult<Match> b = controller.PostMatch(match_error);

            Assert.IsType<CreatedResult>(a.Result);
            Assert.IsType<BadRequestResult>(b.Result);
        }

        [Fact]
        public void DeleteTest()
        {
            IActionResult a = controller.DeleteMatch(2);
            IActionResult b = controller.DeleteMatch(10);
            Assert.IsType<NoContentResult>(a);
            Assert.IsType<NotFoundResult>(b);
        }

        [Fact]
        public void PutTest()
        {
            IActionResult a = controller.PutMatch(1, new Match { Id = 1, HostId = 2, GuestId = 4, Date = DateTime.Parse("2022-12-26 17:30"), Tickets_amount = 1, Price = 75 });
            IActionResult b = controller.PutMatch(2, new Match { Id = 1, HostId = 2, GuestId = 4, Date = DateTime.Parse("2022-12-26 17:30"), Tickets_amount = 12, Price = 35 });
            Assert.IsType<NoContentResult>(a);
            Assert.IsType<BadRequestResult>(b);
        }
    }
}