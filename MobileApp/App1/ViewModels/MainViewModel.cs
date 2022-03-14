using System;
using System.Collections.Generic;
using System.Text;
using App1.Views;
using App.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadRecommendationsCommand { get; }

        public MainViewModel()
        {
            LoadRecommendationsCommand = new Command(async () => await LoadRecommendations());
            DetailedField.OnLoadRecommendations += CurrentAccount.LoadRecommendations;
            DetailedField.OnGetRecommendations += CurrentAccount.GetRecommendations;
            DetailedField.OnGiveFeedback += GiveFeedback;
            CurrentAccount.CustomerInfo.OnFieldsChanged += () => OnPropertyChanged(nameof(Fields));
            CurrentAccount.OnCustomerInfoChanged += () => OnPropertyChanged(nameof(Fields));
        }

        protected List<DetailedField> fields;
        public List<DetailedField> Fields
        {
            get
            {
                if (fields == null)
                    fields = DetailedField.GetDetailedField(CurrentAccount.CustomerInfo.Fields);
                return fields;
            }
            set
            {
                OnPropertyChanged(nameof(Fields));
            }
        }

        private async void GiveFeedback()
        {
            await App.Current.MainPage.DisplayAlert("Рекомендации",
                "Оставить отзыв?", "Да", "Нет");
        }

        private async Task LoadRecommendations()
        {
            fields = DetailedField.GetDetailedField(CurrentAccount.CustomerInfo.Fields);
            IsBusy = false;
            OnPropertyChanged(nameof(Fields));
        }

        public class DetailedField
        {
            public ICommand GiveFeedbackCommand { get; }

            public DetailedField(Field field)
            {
                GiveFeedbackCommand = new Command(OnGiveFeedback);
                FieldItem = field;
                OnLoadRecommendations.Invoke(FieldItem);
            }

            public Dictionary<RecommendationTypes, string> Recommendations = new()
            {
                [RecommendationTypes.Watering] = "wateringCan.png",
                [RecommendationTypes.Fertilizing] = "plant.png",
                [RecommendationTypes.Harvest] = "shovel.png",
                [RecommendationTypes.None] = "ok.png"
            };

            private readonly Dictionary<Plants, string> PlantImages = new()
            {
                [Plants.Carrot] = "carrot.png",
                [Plants.Potato] = "potatoes.png",
                [Plants.Wheat] = "grass.png",
                [Plants.Tomato] = "tomato.png",
                [Plants.Corn] = "corn.png",
                [Plants.None] = "default.png"
            };

            public static event Action OnGiveFeedback;
            public static event Func<Field, string[]> OnLoadRecommendations;
            public static event Func<Field, Recommendation[]> OnGetRecommendations;

            public Field FieldItem { get; init; }

            private Recommendation[] ActualRecommendations
            {
                get => OnGetRecommendations.Invoke(FieldItem);
            }

            public string FirstRecommendation
            {
                get
                {
                    if (ActualRecommendations.Length == 0)
                        return "Можно отдохнуть сегодня!";

                    return ActualRecommendations[0].Value;
                }
            }

            public string FirstRecommendationIcon
            {
                get
                {
                    if (ActualRecommendations.Length == 0)
                        return Recommendations[RecommendationTypes.None];

                    return Recommendations[ActualRecommendations[0].Type];
                }
            }

            public string SecondRecommendation
            {
                get
                {
                    if (ActualRecommendations.Length <= 1)
                        return "Можно отдохнуть сегодня!";

                    return ActualRecommendations[1].Value;
                }
            }

            public string SecondRecommendationIcon
            {
                get
                {
                    if (ActualRecommendations.Length <= 1)
                        return Recommendations[RecommendationTypes.None];

                    return Recommendations[ActualRecommendations[1].Type];
                }
            }

            public string ThirdRecommendation
            {
                get
                {
                    if (ActualRecommendations.Length <= 2)
                        return "Можно отдохнуть сегодня!";

                    return ActualRecommendations[2].Value;
                }
            }


            public string ThirdRecommendationIcon
            {
                get
                {
                    if (ActualRecommendations.Length <= 2)
                        return Recommendations[RecommendationTypes.None];

                    return Recommendations[ActualRecommendations[2].Type];
                }
            }

            public string PlantIcon
            {
                get => PlantImages[FieldItem.Plant];
            }

            public static List<DetailedField> GetDetailedField(List<Field> fields)
            {
                List<DetailedField> list = new();

                foreach (Field field in fields)
                    list.Add(new DetailedField(field));

                return list;
            }
        }
    }
}