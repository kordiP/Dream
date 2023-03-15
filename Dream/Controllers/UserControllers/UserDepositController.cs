using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserDepositController
    {
        private UserRepository userRepository;
        private UserDepositView depositView;
        private User currentUser;

        public UserDepositController(User user)
        {
            userRepository = new UserRepository();
            currentUser = user;
        }
        public decimal Deposit()
        {
            depositView = new UserDepositView(currentUser.Username);
            if (currentUser.Balance is null)
            {
                currentUser.Balance = depositView.Amount;
            }
            else
            {
                currentUser.Balance += depositView.Amount;
            }

            userRepository.Update(currentUser);
            depositView.SuccessfulDeposit((decimal)currentUser.Balance);
            return (decimal)currentUser.Balance;
        }
        public bool IsDepositValid()
        {
            if (depositView.Amount > 0)
            {
                return true;
            }
            else
            {
                depositView.InvalidDeposit();
                return false;
            }
        }
        public decimal Purchase(Game game)
        {
            currentUser.Balance -= game.Price;
            userRepository.Update(currentUser);
            userRepository.Save();
            return game.Price;
        }
    }
}
