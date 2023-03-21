using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserDepositController
    {
        private UserRepository userRepository;
        private UserDepositView depositView;

        private DreamContext context;

        public UserDepositController(DreamContext context)
        {
            this.context = context;

            this.userRepository = new UserRepository(context);
        }
        public decimal Deposit(User user)
        {
            depositView = new UserDepositView(user.Username);

            if(user.Balance is null && IsDepositValid())
            {
                user.Balance = 0;
                user.Balance += depositView.Amount;
            }
            else if (IsDepositValid())
            {
                user.Balance += depositView.Amount;
            }
            else
            {
                depositView.InvalidDeposit();
                return 0;
            }

            userRepository.Update(user);
            depositView.SuccessfulDeposit((decimal)user.Balance);

            return (decimal)user.Balance;
        }
        public bool IsDepositValid()
        {
            if (depositView.Amount >= 0) return true;
            return false;
        }
        public decimal Purchase(Game game, User user)
        {
            user.Balance -= game.Price;

            userRepository.Update(user);

            return game.Price;
        }
    }
}
