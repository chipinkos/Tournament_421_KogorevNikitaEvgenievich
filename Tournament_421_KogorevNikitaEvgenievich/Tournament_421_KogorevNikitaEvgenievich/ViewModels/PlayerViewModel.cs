using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tournament_421_KogorevNikitaEvgenievich.Data;
using Tournament_421_KogorevNikitaEvgenievich.Domain.Commands;
using Tournament_421_KogorevNikitaEvgenievich.Domain.IServices;
using Tournament_421_KogorevNikitaEvgenievich.Domain.Utilities;
using Tournament_421_KogorevNikitaEvgenievich.Properties;

namespace Tournament_421_KogorevNikitaEvgenievich.ViewModels
{
    public class PlayerViewModel : ViewModel
    {
        private readonly TournamentDB_421_KogorevNikitaEvgenievichEntities1 _entities;

        public Player Player { get; set; } = new Player();

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsLoginBlocked)); }
        }
        public bool IsLoginBlocked => !IsLoading;

        private bool _isLogged = false;
        public bool IsLogged
        {
            get { return _isLogged; }
            set { _isLogged = value; OnPropertyChanged(); OnPropertyChanged(nameof(LoginHidden)); }
        }
        public bool LoginHidden => !IsLogged;

        public ICommand LoginCommand { get; }
        public ICommand CreateTeamCommand { get; }
        public ICommand SelectTourCommand { get; }

        public PlayerViewModel(INavService createTeam, INavService selectTour, TournamentDB_421_KogorevNikitaEvgenievichEntities1 entities)
        {
            CreateTeamCommand = new NavCommand(createTeam);
            SelectTourCommand = new NavCommand(selectTour);

            LoginCommand = new RelayAsyncCommand(LoginAsync);

            _entities = entities;
        }

        public async Task LoginAsync(object param)
        {
            try
            {
                IsLoading = true;

                if (param is PasswordBox passwordBox)
                {
                    var password = passwordBox.Password;

                    if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(Player.Login))
                    {
                        MessageBox.Show("Пустые поля");
                        return;
                    }

                    var user = await _entities.Player
                        .FirstOrDefaultAsync(it => it.Login == Player.Login && it.Password == password);

                    if (user is null)
                    {
                        MessageBox.Show("Неверные данные");
                        passwordBox.Password = string.Empty;
                    }

                    Player = user;
                    OnPropertyChanged(nameof(Player));

                    //Settings.Default.UserId = user.Id;
                    //Settings.Default.ModId = 1;
                    Settings.Default.Save();

                    IsLogged = true;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
