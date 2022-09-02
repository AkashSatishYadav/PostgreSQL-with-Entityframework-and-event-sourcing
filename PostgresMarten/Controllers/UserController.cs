using Microsoft.AspNetCore.Mvc;
using PostgresMarten.DataAccess;
using PostgresMarten.Events;
using PostgresMarten.Extensions;
using PostgresMarten.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgresMarten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataStore _dataSource;
        public UserController(IDataStore dataSource)
        {
            _dataSource = dataSource;

        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _dataSource.Context.Users.ToList();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            var user = await _dataSource.Cache.GetRecordAsync<User>(id.ToString());
            if (user == null)
            {
                user = await _dataSource.Context.Users.FindAsync(id);
                _dataSource.Cache.SetRecordAsync<User>(id.ToString(), user);
            }
            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            UserAdded userAdded = new UserAdded()
            {
                UserId = Guid.NewGuid(),
                Id = user.Id,
                UserName = user.Name
            };
            if (!_dataSource.Context.Users.Any())
            {
                _dataSource.EventStorage.StartStream(userAdded);
            }
            else
            {
                _dataSource.EventStorage.AppendStream(userAdded);
            }
            _dataSource.CommitChanges();
            _dataSource.Context.Users.Add(user);
            _dataSource.Context.SaveChanges();
        }

    }
}
