using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсоч
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentMyTestName;
        string currentMyTestDescription;
        user CurrentUser = new user();
        double descriptorSN = 0;
        double descriptorEI = 0;
        double descriptorJP = 0;
        double descriptorTF = 0;
        string type = "";

        List<double> testAnswers = new List<double>();
        List<string> questionsText = new List<string>()
        {
            ////descriptor SN
            "Вы часто делаете что-то из чистого любопытства.",
            "Ваши мысли, как правило,не сосредоточены на реальном мире и его событиям.",
            "Если кто-то быстро не ответил на ваше электронное письмо, вы начинаете переживать, не сказали ли вы что-то неправильно.",
            "Ваш ум всегда переполнен неизведанными идеями и планами.",
            ////descriptor EI
            "Вы часто так погружаетесь в свои мысли, что не замечаете где и с кем вы находитесь.",
            "Вы не любите быть в центре внимания",
            "Интересная книга или видео игра зачастую намного лучше для вас, чем светское мероприятие.",
            "Гуляя на природе вы часто погружаетесь в свои мысли.",
            "Если в комнате много людей, вы держитесь ближе к стене, избегая центра.",
            ////descriptor JP
            "Вам легко оставаться уравновешенным и сконцентрированным даже в напряженной обстановке.",
            "Ваше рабочее место аккуратное, а деятельность организована.",
            "Ваши планы путешествий, как правило, продуманы до мелочей.",
            "Вы больше осторожный планировщик, чем стихийный импровизатор.",
            "Ваш стиль работы можно скорее охарактиризовать как  методичная и организованная деятельность, а не беспорядочные всплески энергии.",
             ////descriptor TF
            "Если бы у вас был свой бизнес, вам бы было очень сложно уволить преданного, но неэффективного сотрудника.",
            "Если ваш друг чем-то огорчен, вы, скорее всего, эмоционально поддержите его, а не посоветуете, как решить проблему.",
            "Вы думаете, что мнение любого человека необходимо уважать, независимо от того, подтверждено ли оно фактами или нет.",
            "Для вас важнее убедиться в том, что никто не огорчился, чем выиграть спор.",
            "Вам нравиться ходить на светские мероприятия, которые связаны со сменой нарядов и ролевыми играми.",
        };

        int questionCounter = 0;
        List<Ellipse> ellipses = new List<Ellipse>();
        List<Ellipse> ellipsesY = new List<Ellipse>();
        List<string> yourQuestionsText = new List<string>();
        int yourQuestionCounter = 0;


        public MainWindow()
        {
            InitializeComponent();
            ellipses.Add(el1);
            ellipses.Add(el2);
            ellipses.Add(el3);
            ellipses.Add(el4);
            ellipses.Add(el5);
            ellipses.Add(el6);
            ellipses.Add(el7);
            ellipses.Add(el8);
            ellipses.Add(el9);
            ellipses.Add(el10);
            ellipses.Add(el11);
            ellipses.Add(el12);
            ellipses.Add(el13);
            ellipses.Add(el14);
            ellipses.Add(el15);
            ellipses.Add(el16);
            ellipses.Add(el17);
            ellipses.Add(el18);
            ellipses.Add(el19);
            ellipses.Add(el20);

            ellipsesY.Add(el1Y);
            ellipsesY.Add(el2Y);
            ellipsesY.Add(el3Y);
            ellipsesY.Add(el4Y);
            ellipsesY.Add(el5Y);
            ellipsesY.Add(el6Y);
            ellipsesY.Add(el7Y);
            ellipsesY.Add(el8Y);
            ellipsesY.Add(el9Y);
            ellipsesY.Add(el10Y);
            ellipsesY.Add(el11Y);
            ellipsesY.Add(el12Y);
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonBegin_Click(object sender, RoutedEventArgs e)
        {
            SignInSignUpPanel.Visibility = Visibility.Visible;
            buttonBegin.Visibility= Visibility.Hidden;
            DoubleAnimation SignInSignUpPanelAnimation = new DoubleAnimation();
            SignInSignUpPanelAnimation.From = SignInSignUpPanel.Opacity;
            SignInSignUpPanelAnimation.To = 1;
            SignInSignUpPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
            SignInSignUpPanel.BeginAnimation(Border.OpacityProperty, SignInSignUpPanelAnimation);
        }

        private void buttonSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignInPanel.Visibility = Visibility.Visible;
            SignInSignUpPanel.Visibility = Visibility.Hidden;
            DoubleAnimation SignInPanelAnimation = new DoubleAnimation();
            SignInPanelAnimation.From = SignInPanel.Opacity;
            SignInPanelAnimation.To = 1;
            SignInPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
            SignInPanel.BeginAnimation(Border.OpacityProperty, SignInPanelAnimation);
        }

        private void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpPanel.Visibility = Visibility.Visible;
            SignInSignUpPanel.Visibility = Visibility.Hidden;
            DoubleAnimation SignUpPanelAnimation = new DoubleAnimation();
            SignUpPanelAnimation.From = SignUpPanel.Opacity;
            SignUpPanelAnimation.To = 1;
            SignUpPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
            SignUpPanel.BeginAnimation(Border.OpacityProperty, SignUpPanelAnimation);
        }

        private void buttonSignUp2_Click(object sender, RoutedEventArgs e)
        {
            using (sixteenPersDB db = new sixteenPersDB())
            {
                Regex regexLogin = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
                Regex regexPassword = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$");
                string login = TextBoxSignUpLogin.Text;
                string password = TextBoxSignUpPassword.Text;
                string passwordRepeat = TextBoxSignUpPasswordRepeat.Text;
                if (regexLogin.IsMatch(login))
                {
                    if (regexPassword.IsMatch(password))
                    {
                        if (password == passwordRepeat)
                        {
                            user NewUser = new user { login = login, password = password };
                            db.users.Add(NewUser);
                            CurrentUser = NewUser;
                            db.SaveChanges();
                            SuccessfulRegistrationPanel.Visibility = Visibility.Visible;
                            SignUpPanel.Visibility = Visibility.Hidden;
                            DoubleAnimation SuccessfulRegistrationPanelAnimation = new DoubleAnimation();
                            SuccessfulRegistrationPanelAnimation.From = SuccessfulRegistrationPanel.Opacity;
                            SuccessfulRegistrationPanelAnimation.To = 1;
                            SuccessfulRegistrationPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                            SuccessfulRegistrationPanel.BeginAnimation(Border.OpacityProperty, SuccessfulRegistrationPanelAnimation);
                        }
                        else
                        {
                            ErrorSignUp.Text = "Пароли не совпадают";
                        }
                    }
                    else
                    {
                        ErrorSignUp.Text = "Пароль должен содержать строчные и прописные латинские буквы, цифры";
                    }
                }
                else
                {
                    ErrorSignUp.Text = "Имя пользователя должно содержать 2-20 символов: буквы и цифры, первый символ обязательно букв";
                }
            }
            }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Visible;
            SuccessfulRegistrationPanel.Visibility = Visibility.Hidden;
            DoubleAnimation  MenuPanelAnimation = new DoubleAnimation();
            MenuPanelAnimation.From = MenuPanel.Opacity;
            MenuPanelAnimation.To = 1;
            MenuPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
            SuccessfulRegistrationPanel.BeginAnimation(Border.OpacityProperty, MenuPanelAnimation);

        }

        private void buttonPersonalities_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Hidden;
            buttonAnalytics.Visibility = Visibility.Visible;
            buttonGuardians.Visibility = Visibility.Visible;
            buttonCrawlers.Visibility = Visibility.Visible;
            buttonDiplomats.Visibility = Visibility.Visible;

        }

        private void radio3_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonSignIn2_Click(object sender, RoutedEventArgs e)
        {
            using (sixteenPersDB db = new sixteenPersDB())
            {
                string login = TextBoxSignInLogin.Text;
                string password = TextBoxSignInPassword.Text;
                user User = new user { login = login, password = password };////ask
                bool flagLogin=false;
                bool flagPassword = false;
                foreach (user u in db.users)
                {
                    if (u.login == login)
                    {
                        flagLogin = true;
                        if (u.password == password)
                        {
                            flagPassword = true;
                            CurrentUser = User;
                            SignInPanel.Visibility = Visibility.Hidden;
                            MenuPanel.Visibility = Visibility.Visible;
                            DoubleAnimation MenuPanelAnimation = new DoubleAnimation();
                            MenuPanelAnimation.From = MenuPanel.Opacity;
                            MenuPanelAnimation.To = 1;
                            MenuPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                            MenuPanel.BeginAnimation(Border.OpacityProperty, MenuPanelAnimation);

                        }
                    }
                }
                if (!flagLogin)
                {
                    ErrorSignIn.Text = "Нет такого пользователя";
                }
                if (!flagPassword&& flagLogin)
                {
                    ErrorSignIn.Text = "Неверный пароль";
                }

            }
            }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            
            MenuPanel.Visibility = Visibility.Hidden;
            question.Visibility = Visibility.Visible;
            radioPanel.Visibility = Visibility.Visible;
            buttonNextQuestion.Visibility = Visibility.Visible;
            beginTestPanel.Visibility = Visibility.Visible;
            statusBar.Visibility = Visibility.Visible;
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrame = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrame2 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrame3 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrame4 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrame.KeyTime = TimeSpan.FromSeconds(0);
            linearDoubleKeyFrame.Value = 0;
            linearDoubleKeyFrame2.KeyTime = TimeSpan.FromSeconds(0.3);
            linearDoubleKeyFrame2.Value = 1;
            linearDoubleKeyFrame3.KeyTime = TimeSpan.FromSeconds(3.5);
            linearDoubleKeyFrame3.Value = 1;
            linearDoubleKeyFrame4.KeyTime = TimeSpan.FromSeconds(3.8);
            linearDoubleKeyFrame4.Value = 0;
            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrame);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrame2);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrame3);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrame4);
            beginTestPanel.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFrames);


            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesQuestionRadioPanel = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion2 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameQuestion.KeyTime = TimeSpan.FromSeconds(3.8);
            linearDoubleKeyFrameQuestion.Value = 0;
            linearDoubleKeyFrameQuestion2.KeyTime = TimeSpan.FromSeconds(4.3);
            linearDoubleKeyFrameQuestion2.Value = 1;
            doubleAnimationUsingKeyFramesQuestionRadioPanel.KeyFrames.Add(linearDoubleKeyFrameQuestion);
            doubleAnimationUsingKeyFramesQuestionRadioPanel.KeyFrames.Add(linearDoubleKeyFrameQuestion2);
            radioPanel.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesQuestionRadioPanel);

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesQuestion = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion3 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion4 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameQuestion3.KeyTime = TimeSpan.FromSeconds(3.8);
            linearDoubleKeyFrameQuestion3.Value = 0;
            linearDoubleKeyFrameQuestion4.KeyTime = TimeSpan.FromSeconds(4.3);
            linearDoubleKeyFrameQuestion4.Value = 1;
            doubleAnimationUsingKeyFramesQuestion.KeyFrames.Add(linearDoubleKeyFrameQuestion3);
            doubleAnimationUsingKeyFramesQuestion.KeyFrames.Add(linearDoubleKeyFrameQuestion4);
            question.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesQuestion);


            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesNextQuestion = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion5 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion6 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameQuestion5.KeyTime = TimeSpan.FromSeconds(3.8);
            linearDoubleKeyFrameQuestion5.Value = 0;
            linearDoubleKeyFrameQuestion6.KeyTime = TimeSpan.FromSeconds(4.3);
            linearDoubleKeyFrameQuestion6.Value = 1;
            doubleAnimationUsingKeyFramesNextQuestion.KeyFrames.Add(linearDoubleKeyFrameQuestion5);
            doubleAnimationUsingKeyFramesNextQuestion.KeyFrames.Add(linearDoubleKeyFrameQuestion6);
            buttonNextQuestion.BeginAnimation(Button.OpacityProperty, doubleAnimationUsingKeyFramesNextQuestion);

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesStatusBar = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion7 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameQuestion8 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameQuestion7.KeyTime = TimeSpan.FromSeconds(3.8);
            linearDoubleKeyFrameQuestion7.Value = 0;
            linearDoubleKeyFrameQuestion8.KeyTime = TimeSpan.FromSeconds(4.3);
            linearDoubleKeyFrameQuestion8.Value = 1;
            doubleAnimationUsingKeyFramesStatusBar.KeyFrames.Add(linearDoubleKeyFrameQuestion7);
            doubleAnimationUsingKeyFramesStatusBar.KeyFrames.Add(linearDoubleKeyFrameQuestion8);
            statusBar.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesStatusBar);
        }

        private void buttonNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            bool flag=false;
            if(questionCounter<=questionsText.Count)
            {
                if (radio0.IsChecked == true) { testAnswers.Add(1);flag = true; }
                if (radio1.IsChecked == true) { testAnswers.Add(0.6); flag = true; }
                if (radio2.IsChecked == true) { testAnswers.Add(0.3); flag = true; }
                if (radio3.IsChecked == true) { testAnswers.Add(0); flag = true; }
                if (radio4.IsChecked == true) { testAnswers.Add(-0.3); flag = true; }
                if (radio5.IsChecked == true) { testAnswers.Add(-0.6); flag = true; }
                if (radio6.IsChecked == true) { testAnswers.Add(-1); flag = true; }
            }

            if (flag)
            {
                ellipses[questionCounter].Fill = new SolidColorBrush(Colors.Green);
                radio0.IsChecked = false;
                radio2.IsChecked = false;
                radio3.IsChecked = false;
                radio4.IsChecked = false;
                radio5.IsChecked = false;
                radio6.IsChecked = false;
                if(questionCounter!= questionsText.Count) { question.Text = questionsText[questionCounter]; }
                if (questionCounter== questionsText.Count-1)
                {
                    buttonNextQuestion.Visibility = Visibility.Hidden;
                    buttonLastQuestion.Visibility = Visibility.Visible;
                }
                if (questionCounter == questionsText.Count)
                {
                    buttonNextQuestion.Visibility = Visibility.Hidden;
                    buttonLastQuestion.Visibility = Visibility.Hidden;
                    question.Visibility = Visibility.Hidden;
                    radioPanel.Visibility = Visibility.Hidden;
                    Logo.Visibility = Visibility.Hidden;
                    statusBar.Visibility = Visibility.Hidden;
                    ////////
                    for (int i=0; i < 5; i++){descriptorSN += testAnswers[i];}
                    for (int i  = 5; i < 10; i++){descriptorEI += testAnswers[i];}
                    for (int i = 10; i < 15; i++){descriptorJP += testAnswers[i];}
                    for (int i = 15; i < 20; i++){descriptorTF += testAnswers[i];}
                    if (descriptorEI < 0) { type += "E"; } else { type += "I"; }
                    if (descriptorSN < 0){type += "S";}else { type += "N"; }
                    if (descriptorTF < 0) { type += "T"; } else { type += "F"; }
                    if (descriptorJP < 0) { type += "P"; } else { type += "J"; }
                    using (sixteenPersDB db = new sixteenPersDB())
                    {
                        result currentResult = new result() { user_login = CurrentUser.login, descriptor_SN=(float?)descriptorSN,
                            descriptor_EI = (float?)descriptorEI, descriptor_JP = (float?)descriptorJP, descriptor_TF = (float?)descriptorTF, result_type=type };
                        db.results.Add(currentResult);
                        db.SaveChanges();

                    }
                    resultPanel.Visibility = Visibility.Visible;
                    DoubleAnimation resultPanelAnimation = new DoubleAnimation();
                    resultPanelAnimation.From = resultPanel.Opacity;
                    resultPanelAnimation.To = 1;
                    resultPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                    resultPanel.BeginAnimation(StackPanel.OpacityProperty, resultPanelAnimation);

                    infoPanel.Visibility = Visibility.Visible;
                    DoubleAnimation infoPanelAnimation = new DoubleAnimation();
                    infoPanelAnimation.From = infoPanel.Opacity;
                    infoPanelAnimation.To = 1;
                    infoPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                    infoPanel.BeginAnimation(StackPanel.OpacityProperty, infoPanelAnimation);

                    statInfoPanel.Visibility = Visibility.Visible;
                    DoubleAnimation statInfoPanelAnimation = new DoubleAnimation();
                    statInfoPanelAnimation.From = statInfoPanel.Opacity;
                    statInfoPanelAnimation.To = 1;
                    statInfoPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                    statInfoPanel.BeginAnimation(StackPanel.OpacityProperty, statInfoPanelAnimation);

                    int descEIWIDTH = 210-(int)(105 + 105 * (descriptorEI / 5));

                    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesDescriptorEI = new DoubleAnimationUsingKeyFrames();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorEI = new LinearDoubleKeyFrame();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorEI2 = new LinearDoubleKeyFrame();
                    linearDoubleKeyFrameDescriptorEI.KeyTime = TimeSpan.FromSeconds(0.5);
                    linearDoubleKeyFrameDescriptorEI.Value = 0;
                    linearDoubleKeyFrameDescriptorEI2.KeyTime = TimeSpan.FromSeconds(1);
                    linearDoubleKeyFrameDescriptorEI2.Value = descEIWIDTH;
                    doubleAnimationUsingKeyFramesDescriptorEI.KeyFrames.Add(linearDoubleKeyFrameDescriptorEI);
                    doubleAnimationUsingKeyFramesDescriptorEI.KeyFrames.Add(linearDoubleKeyFrameDescriptorEI2);
                    DescriptorEI.BeginAnimation(Border.WidthProperty, doubleAnimationUsingKeyFramesDescriptorEI);


                    int descSNWIDTH = (int)(105 + 105 * (descriptorSN / 5));

                    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesDescriptorSN = new DoubleAnimationUsingKeyFrames();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorSN = new LinearDoubleKeyFrame();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorSN2 = new LinearDoubleKeyFrame();
                    linearDoubleKeyFrameDescriptorSN.KeyTime = TimeSpan.FromSeconds(1);
                    linearDoubleKeyFrameDescriptorSN.Value = 0;
                    linearDoubleKeyFrameDescriptorSN2.KeyTime = TimeSpan.FromSeconds(1.5);
                    linearDoubleKeyFrameDescriptorSN2.Value = descSNWIDTH;
                    doubleAnimationUsingKeyFramesDescriptorSN.KeyFrames.Add(linearDoubleKeyFrameDescriptorSN);
                    doubleAnimationUsingKeyFramesDescriptorSN.KeyFrames.Add(linearDoubleKeyFrameDescriptorSN2);
                    DescriptorSN.BeginAnimation(Border.WidthProperty, doubleAnimationUsingKeyFramesDescriptorSN);

                    int descTFWIDTH = 210-(int)(105 + 105 * (descriptorTF / 5));

                    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesDescriptorTF = new DoubleAnimationUsingKeyFrames();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorTF = new LinearDoubleKeyFrame();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorTF2 = new LinearDoubleKeyFrame();
                    linearDoubleKeyFrameDescriptorTF.KeyTime = TimeSpan.FromSeconds(1.5);
                    linearDoubleKeyFrameDescriptorTF.Value = 0;
                    linearDoubleKeyFrameDescriptorTF2.KeyTime = TimeSpan.FromSeconds(2);
                    linearDoubleKeyFrameDescriptorTF2.Value = descSNWIDTH;
                    doubleAnimationUsingKeyFramesDescriptorTF.KeyFrames.Add(linearDoubleKeyFrameDescriptorTF);
                    doubleAnimationUsingKeyFramesDescriptorTF.KeyFrames.Add(linearDoubleKeyFrameDescriptorTF2);
                    DescriptorTF.BeginAnimation(Border.WidthProperty, doubleAnimationUsingKeyFramesDescriptorTF);

                    int descJPWIDTH = 210 - (int)(105 + 105 * (descriptorTF / 5));

                    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesDescriptorJP = new DoubleAnimationUsingKeyFrames();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorJP = new LinearDoubleKeyFrame();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorJP2 = new LinearDoubleKeyFrame();
                    linearDoubleKeyFrameDescriptorJP.KeyTime = TimeSpan.FromSeconds(2);
                    linearDoubleKeyFrameDescriptorJP.Value = 0;
                    linearDoubleKeyFrameDescriptorJP2.KeyTime = TimeSpan.FromSeconds(2.5);
                    linearDoubleKeyFrameDescriptorJP2.Value = descSNWIDTH;
                    doubleAnimationUsingKeyFramesDescriptorJP.KeyFrames.Add(linearDoubleKeyFrameDescriptorJP);
                    doubleAnimationUsingKeyFramesDescriptorJP.KeyFrames.Add(linearDoubleKeyFrameDescriptorJP2);
                    DescriptorJP.BeginAnimation(Border.WidthProperty, doubleAnimationUsingKeyFramesDescriptorJP);


                }
                questionCounter++;
                flag = false;
            }
            
        }

        private void buttonSectionBasic_MouseEnter(object sender, MouseEventArgs e)
        {
            textBlockSectionBasic.Visibility = Visibility.Visible;
            DoubleAnimation textBlockSectionBasicAnimation = new DoubleAnimation();
            textBlockSectionBasicAnimation.From = textBlockSectionBasic.Opacity;
            textBlockSectionBasicAnimation.To = 1;
            textBlockSectionBasicAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionBasic.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionBasicAnimation);
        }

        private void buttonSectionBasic_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation textBlockSectionBasicAnimation = new DoubleAnimation();
            textBlockSectionBasicAnimation.From = textBlockSectionBasic.Opacity;
            textBlockSectionBasicAnimation.To = 0;
            textBlockSectionBasicAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionBasic.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionBasicAnimation);
        }

        private void buttonSectionAdvantages_MouseEnter(object sender, MouseEventArgs e)
        {
            textBlockSectionAdvantages.Visibility = Visibility.Visible;
            DoubleAnimation textBlockSectionAdvantagesAnimation = new DoubleAnimation();
            textBlockSectionAdvantagesAnimation.From = textBlockSectionAdvantages.Opacity;
            textBlockSectionAdvantagesAnimation.To = 1;
            textBlockSectionAdvantagesAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionAdvantages.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionAdvantagesAnimation);
        }

        private void buttonSectionAdvantages_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation textBlockSectionAdvantagesAnimation = new DoubleAnimation();
            textBlockSectionAdvantagesAnimation.From = textBlockSectionBasic.Opacity;
            textBlockSectionAdvantagesAnimation.To = 0;
            textBlockSectionAdvantagesAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionAdvantages.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionAdvantagesAnimation);
        }

        private void buttonSectionsDisAdvantages_MouseEnter(object sender, MouseEventArgs e)
        {
            textBlockSectionsDisAdvantages.Visibility = Visibility.Visible;
            DoubleAnimation textBlockSectionsDisAdvantagesAnimation = new DoubleAnimation();
            textBlockSectionsDisAdvantagesAnimation.From = textBlockSectionsDisAdvantages.Opacity;
            textBlockSectionsDisAdvantagesAnimation.To = 1;
            textBlockSectionsDisAdvantagesAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionsDisAdvantages.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionsDisAdvantagesAnimation);
        }

        private void buttonSectionsDisAdvantages_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation textBlockSectionsDisAdvantagesAnimation = new DoubleAnimation();
            textBlockSectionsDisAdvantagesAnimation.From = textBlockSectionsDisAdvantages.Opacity;
            textBlockSectionsDisAdvantagesAnimation.To = 0;
            textBlockSectionsDisAdvantagesAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionsDisAdvantages.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionsDisAdvantagesAnimation);
        }

        private void buttonSectionAnother_MouseEnter(object sender, MouseEventArgs e)
        {
            textBlockSectionAnother.Visibility = Visibility.Visible;
            DoubleAnimation textBlockSectionAnotherAnimation = new DoubleAnimation();
            textBlockSectionAnotherAnimation.From = textBlockSectionAnother.Opacity;
            textBlockSectionAnotherAnimation.To = 1;
            textBlockSectionAnotherAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionAnother.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionAnotherAnimation);
        }

        private void buttonSectionAnother_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation textBlockSectionAnotherAnimation = new DoubleAnimation();
            textBlockSectionAnotherAnimation.From = textBlockSectionAnother.Opacity;
            textBlockSectionAnotherAnimation.To = 0;
            textBlockSectionAnotherAnimation.Duration = TimeSpan.FromSeconds(0.3);
            textBlockSectionAnother.BeginAnimation(TextBlock.OpacityProperty, textBlockSectionAnotherAnimation);
        }



        private void buttonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            nameCreatePanel.Visibility = Visibility.Visible;

            DoubleAnimation nameCreatePanelAnimation = new DoubleAnimation();
            nameCreatePanelAnimation.From = nameCreatePanel.Opacity;
            nameCreatePanelAnimation.To = 1;
            nameCreatePanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
            nameCreatePanel.BeginAnimation(StackPanel.OpacityProperty, nameCreatePanelAnimation);

            beginCreatePanel.Visibility = Visibility.Hidden;



            }

        private void nextYourQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (yourQuestionCounter <= 9)
            {
                if (yourQuestion.Text != "Введите свой вопрос")
                {
                    yourQuestionsText.Add(yourQuestion.Text);
                    yourQuestion.Text = "Введите свой вопрос";
                    ellipsesY[yourQuestionCounter].Fill = new SolidColorBrush(Colors.Green);
                    yourQuestionCounter++;
                }
            }
            else
            {
                if (yourQuestion.Text != "Введите свой вопрос")
                {
                    yourQuestionsText.Add(yourQuestion.Text);
                    yourQuestion.Text = "Введите свой вопрос";
                    ellipsesY[yourQuestionCounter].Fill = new SolidColorBrush(Colors.Green);
                    saveTest.Visibility = Visibility.Visible;
                    nextYourQuestion.Visibility = Visibility.Hidden;
                }
            }
          
        }

        private void buttonMyTests_Click(object sender, RoutedEventArgs e)
        {
            beginCreatePanel.Visibility = Visibility.Visible;
            DoubleAnimation beginCreatePanelAnimation = new DoubleAnimation();
            beginCreatePanelAnimation.From = beginCreatePanel.Opacity;
            beginCreatePanelAnimation.To = 1;
            beginCreatePanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
            beginCreatePanel.BeginAnimation(StackPanel.OpacityProperty, beginCreatePanelAnimation);
            MenuPanel.Visibility = Visibility.Hidden;
        }

        private void buttonNextCreateTest_Click(object sender, RoutedEventArgs e)
        {
            if (yourTestName.Text!= "Введите название здесь.") {
                descriptionCreatePanel.Visibility = Visibility.Visible;
                currentMyTestName = yourTestName.Text;

                DoubleAnimation descriptionCreatePanelAnimation = new DoubleAnimation();
                descriptionCreatePanelAnimation.From = descriptionCreatePanel.Opacity;
                descriptionCreatePanelAnimation.To = 1;
                descriptionCreatePanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                descriptionCreatePanel.BeginAnimation(StackPanel.OpacityProperty, descriptionCreatePanelAnimation);


                nameCreatePanel.Visibility = Visibility.Hidden;
            }
        }

        private void buttonNextNextCreateTest_Click(object sender, RoutedEventArgs e)
        {
            if (yourTestDescription.Text != "Введите описание здесь.")
            {
                statusDynamicBar.Visibility = Visibility.Visible;
                nextYourQuestion.Visibility = Visibility.Visible;
                yourQuestion.Visibility = Visibility.Visible;
                currentMyTestDescription = yourTestDescription.Text;
                descriptionCreatePanel.Visibility = Visibility.Hidden;

                DoubleAnimation statusDynamicBarAnimation = new DoubleAnimation();
                statusDynamicBarAnimation.From = statusDynamicBar.Opacity;
                statusDynamicBarAnimation.To = 1;
                statusDynamicBarAnimation.Duration = TimeSpan.FromSeconds(0.3);
                statusDynamicBar.BeginAnimation(StackPanel.OpacityProperty, statusDynamicBarAnimation);

                DoubleAnimation nextYourQuestionAnimation = new DoubleAnimation();
                nextYourQuestionAnimation.From = nextYourQuestion.Opacity;
                nextYourQuestionAnimation.To = 1;
                nextYourQuestionAnimation.Duration = TimeSpan.FromSeconds(0.3);
                nextYourQuestion.BeginAnimation(Button.OpacityProperty, nextYourQuestionAnimation);

                DoubleAnimation yourQuestionAnimation = new DoubleAnimation();
                yourQuestionAnimation.From = nextYourQuestion.Opacity;
                yourQuestionAnimation.To = 1;
                yourQuestionAnimation.Duration = TimeSpan.FromSeconds(0.3);
                yourQuestion.BeginAnimation(TextBox.OpacityProperty, nextYourQuestionAnimation);
            }
        }
    }


}

