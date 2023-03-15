using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserDepositController
    {
        private UserRepository userRepository;
        private UserDepositView depositView;

        public UserDepositController()
        {
            userRepository = new UserRepository();
        }
        public decimal Deposit(User user)
        {
            depositView = new UserDepositView(user.Username);
            if (user.Balance is null && IsDepositValid())
            {
                user.Balance = depositView.Amount;
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
            if (depositView.Amount >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public decimal Purchase(Game game, User user)
        {
            user.Balance -= game.Price;
            userRepository.Update(user);
            userRepository.Save();
            return game.Price;
        }
    }
}
