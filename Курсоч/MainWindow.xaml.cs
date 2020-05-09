using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        List<string> persDescriptions = new List<string>()
        {
           "Правила, ограничения и традиции — предмет ненависти для Стратегов, все должно быть открытым для оспаривания и переоценки,и если Стратеги видят способ, они часто действуют по собственной инициативе внедряя свои технически превосходящие,иногда безэмоциональные и почти всегда оригинальные методы и идеи.",
           "Ученым постоянно мешает всеобъемлющий страх неудачи. Ученые настолько склонны заново переоценивать свои собственные мысли, беспокоясь о том, не пропустили ли они важный элемент головоломки, что они могут впасть в стагнацию, потеряться в неконкретном мире, где они не могут истинно приложить свои идеи.",
           "За столом переговоров, будь то в корпоративной среде или при покупке автомобиля, Командиры являются доминантами, безжалостными и неумолимыми. Это не потому, что они являются бессердечными или злобными, скорее всего, это можно объяснить тем, что Командиры просто обожают сложности, битвы умов, присущие такой обстановке",
           "Полемист — величайший адвокат дьявола, который успешно разрушает все аргументы и мнения на мелкие составляющие, пуская их по ветру, чтобы все видели. В отличие от более целеустремленных личностей Полемисты делают это все не потому, что пытаются достичь какой-то стратегической задачи или глубинной цели, а просто для удовольствия. Никто так не обожает процесс ментального поединка, как Полемисты.",
           "Активисты обладают уникальной комбинацией черт: несмотря на вежливость у них есть четкая точка зрения, и они будут неутомимо бороться за идею, в которую верят. Люди, которые относятся к этому типу личности, решительны и упрямы, однако они редко используют эту энергию для личной выгоды. Активисты действуют с креативностью, воображением, убежденностью и чувствительностью, они действуют для создания баланса, а не преимущества.",
           "Посредники — истинные идеалисты, всегда ищущие искру добра даже в худших из людей или событий в попытке улучшить ситуацию. В то время, как многие воспринимают их как спокойных, сдержанных, даже стеснительных людей, в Посредниках горит внутреннее пламя и страсть, которые могут в какой-то момент засиять. Составляя всего 4% от населения, личности типа «Посредник» часто рискуют остаться непонятыми — но, когда они находят думающих схожим образом людей, с которыми они проводят время, появившаяся гармония становится источником радости и вдохновения.",
           "Людей привлекают сильные личности, а Тренеры излучают естественность, заботу и альтруизм, не боятся встать и высказаться, когда это необходимо. Для них естественно и легко общаться с другими, особенно лично, и их проницательный характер помогает им дотянуться до каждого ума, путем ли представления фактов или логических заключений или чистым эмоциональным убеждением. Тренеры легко понимают мотивы людей и видят на первый взгляд несвязанные события.",
           "Борец — настоящий свободный дух. Борцы часто являются заводилами на вечеринках, однако они не сильно заинтересованы в шумихе и не склонны к наслаждению моментом, они наслаждаются социальными и эмоциональными контактами с другими. Очаровательные, независимые, энергичные и сострадательные — те 7 % населения, которые они составляют, можно, безусловно, почувствовать в любой толпе.",
           "Тип личности Администратор считается одним из наиболее распространенных и составляет 13% населения. Определяющие характеристики данного типа — целостность, практическая логика и неустанное следование своему долгу — делают Администраторов жизненно важным ядром многих семей и организаций, в которых ценятся традиции, правила и стандарты, например, в юридических компаниях, правительственных организациях или в армии.",
           "Тип личности Защитник весьма уникален, поскольку многие из их способностей противоречат их индивидуальным чертам. Чуткие Защитники могут прийти в ярость, если необходимо защитить свою семью или друзей; спокойные и скрытные, они, как правило, обладают хорошо развитыми навыками общения с людьми и крепких социальных взаимоотношений; и хотя они стремятся к безопасности и стабильности, такие типы личности могут быть весьма открытыми к изменениям, если чувствуют, что их понимают и уважают.",
           "Менеджеры представляют закон и порядок, они применяют свое понимание того, что правильно и неправильно, что приемлемо в обществе, а что нет. Они скрепляют семьи и сообщества. Твердо веря в честность, прилежность и порядочность, людей с типом личности Менеджера ценят за четкие советы и разумные инструкции, и они с удовольствием ведут за собой через сложные препятствия. Гордясь своей способностью объединять людей, Менеджеры часто становятся общественными организаторами.",
           "В старших классах Консулы — это чирлидеры и квотербэки, которые задают тон, находятся в центре внимания и ведут свои команды к победе и славе. Позже по жизни Консулы продолжают пользоваться поддержкой своих друзей и любимых, организовывая социальные встречи и делая все возможное для того, чтобы убедиться, что все счастливы.",
           "Виртуозы исследуют идеи с помощью их сборки, отладки, испытаний и вынесения финального заключения. Им нравится, когда другие интересуются их проектами, и иногда они даже позволяют их потрогать и попробовать. Конечно, только при условии, что эти люди не вступают в противоречие с принципами и свободой Виртуоза и чем-либо интересны для него.",
           "Артисты живут в красочном, чувственном мире, наполненном идеями и общением с людьми. Артисты любят по-новому истолковывать такие связи, заново изобретая и экспериментируя как с собой, так и с новыми перспективами. Никакие другие типы личности не изучают таким образом окружающий мир и не экспериментируют с ним. Это создает ощущение спонтанности, и поэтому Артистов часто считают непредсказуемыми даже их друзья и близкие.",
           "Теория, абстрактные концепции и тяжеловесные дискуссии о глобальных проблемах и их последствиях не заинтересуют Дельцов надолго. Эти люди любят живой разговор с определенной толикой интеллектуальности, но о том, что происходит прямо сейчас, а еще лучше вместо разговора пойти и заняться делом. Дельцы прыгают, не посмотрев вниз, исправляют свои ошибки по пути, а не сидят без движения, готовя запасы на случай нехватки и разрабатывая пути возможного отступления.",
           "Они часто настолько сконцентрированы на сиюминутных удовольствиях, что забывают об обязанностях и ответственности, которыми зарабатывается эта роскошь. Сложный анализ, однообразные задачи и сравнение статистических данных с реальностью — это нелегкие задачи для Развлекателей. Они скорее предпочли бы воспользоваться удачей или шансом, или просто попросить помощи у кого-нибудь из своего обширного списка друзей.",
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
                            opacityAnimateBorder(SuccessfulRegistrationPanel);

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
            opacityAnimateBorder(MenuPanel);

        }

        private void buttonPersonalities_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Hidden;
            DoubleAnimation fourPersGroupsAnimation = new DoubleAnimation();
            fourPersGroupsAnimation.From = 0;
            fourPersGroupsAnimation.To = 1;
            fourPersGroupsAnimation.Duration = TimeSpan.FromSeconds(0.3);
            fourPersGroups.BeginAnimation(StackPanel.OpacityProperty, fourPersGroupsAnimation);
            fourPersGroups.Visibility = Visibility.Visible;
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
                    defineYourPersonality(type);
                    
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

                    int descEIWIDTH = (int)(105 + 105 * (descriptorEI / 5));

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


                    int descSNWIDTH = 210-(int)(105 + 105 * (descriptorSN / 5));

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
                    linearDoubleKeyFrameDescriptorTF2.Value = descTFWIDTH;
                    doubleAnimationUsingKeyFramesDescriptorTF.KeyFrames.Add(linearDoubleKeyFrameDescriptorTF);
                    doubleAnimationUsingKeyFramesDescriptorTF.KeyFrames.Add(linearDoubleKeyFrameDescriptorTF2);
                    DescriptorTF.BeginAnimation(Border.WidthProperty, doubleAnimationUsingKeyFramesDescriptorTF);

                    int descJPWIDTH = (int)(105 + 105 * (descriptorJP / 5));

                    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesDescriptorJP = new DoubleAnimationUsingKeyFrames();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorJP = new LinearDoubleKeyFrame();
                    LinearDoubleKeyFrame linearDoubleKeyFrameDescriptorJP2 = new LinearDoubleKeyFrame();
                    linearDoubleKeyFrameDescriptorJP.KeyTime = TimeSpan.FromSeconds(2);
                    linearDoubleKeyFrameDescriptorJP.Value = 0;
                    linearDoubleKeyFrameDescriptorJP2.KeyTime = TimeSpan.FromSeconds(2.5);
                    linearDoubleKeyFrameDescriptorJP2.Value = descJPWIDTH;
                    doubleAnimationUsingKeyFramesDescriptorJP.KeyFrames.Add(linearDoubleKeyFrameDescriptorJP);
                    doubleAnimationUsingKeyFramesDescriptorJP.KeyFrames.Add(linearDoubleKeyFrameDescriptorJP2);
                    DescriptorJP.BeginAnimation(Border.WidthProperty, doubleAnimationUsingKeyFramesDescriptorJP);
                }
                questionCounter++;
                flag = false;
            }
            
        }

        public void defineYourPersonality(string type)
        {
            if (type =="INTJ") { resultPers.Text = "Стратег"; ArchitectRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[0]; }
            if (type == "INTP") { resultPers.Text = "Учёный"; LogicianRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[1]; }
            if (type == "ENTJ") { resultPers.Text = "Командир"; CommanderRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[2]; }
            if (type == "ENTP") { resultPers.Text = "Полемист"; DebaterRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[3]; }
            if (type == "INFJ") { resultPers.Text = "Активист"; AdvocateRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[4]; }
            if (type == "INFP") { resultPers.Text = "Посредник"; MediatorRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[5]; }
            if (type == "ENFJ") { resultPers.Text = "Протагонист"; ProtagonistRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[6]; }
            if (type == "ENFP") { resultPers.Text = "Борец"; CampaignerRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[7]; }
            if (type == "ISTJ") { resultPers.Text = "Администратор"; LogisticianRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[8]; }
            if (type == "ISFJ") { resultPers.Text = "Защитник"; DefenderRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[9]; }
            if (type == "ESTJ") { resultPers.Text = "Менеджер"; ExecutiveRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[10]; }
            if (type == "ESFJ") { resultPers.Text = "Консул"; ConsulRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[11]; }
            if (type == "ISTP") { resultPers.Text = "Виртуоз"; VirtuosoRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[12]; }
            if (type == "ISFP") { resultPers.Text = "Искатель"; AdventurerRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[13]; }
            if (type == "ESTP") { resultPers.Text = "Делец"; EnterteinerRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[14]; }
            if (type == "ESFP") { resultPers.Text = "Увеселитель"; EnterteinerRes.Visibility = Visibility.Visible; resultDescriptionPers.Text = persDescriptions[15]; }
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
                    toResultsYourTest.Visibility = Visibility.Visible;
                    nextYourQuestion.Visibility = Visibility.Hidden;
                }
            }
          
        }

        private void buttonMyTests_Click(object sender, RoutedEventArgs e)
        {
            generateYourTestsBlocks();
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

        private void toResultsYourTest_Click(object sender, RoutedEventArgs e)
        {
            resultsYourTestPanel.Visibility = Visibility.Visible;
            saveTest.Visibility = Visibility.Visible;
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult1 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult1 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult12 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult1.KeyTime = TimeSpan.FromSeconds(0);
            linearDoubleKeyFrameResult1.Value = result1.Opacity;
            linearDoubleKeyFrameResult12.KeyTime = TimeSpan.FromSeconds(0.2);
            linearDoubleKeyFrameResult12.Value = 1;
            doubleAnimationUsingKeyFramesResult1.KeyFrames.Add(linearDoubleKeyFrameResult1);
            doubleAnimationUsingKeyFramesResult1.KeyFrames.Add(linearDoubleKeyFrameResult12);
            result1.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult1);

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult2 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult2 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult22 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult2.KeyTime = TimeSpan.FromSeconds(0.2);
            linearDoubleKeyFrameResult2.Value = result2.Opacity;
            linearDoubleKeyFrameResult22.KeyTime = TimeSpan.FromSeconds(0.4);
            linearDoubleKeyFrameResult22.Value = 1;
            doubleAnimationUsingKeyFramesResult2.KeyFrames.Add(linearDoubleKeyFrameResult2);
            doubleAnimationUsingKeyFramesResult2.KeyFrames.Add(linearDoubleKeyFrameResult22);
            result2.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult2);


            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult3 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult3 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult32 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult3.KeyTime = TimeSpan.FromSeconds(0.4);
            linearDoubleKeyFrameResult3.Value = result3.Opacity;
            linearDoubleKeyFrameResult32.KeyTime = TimeSpan.FromSeconds(0.6);
            linearDoubleKeyFrameResult32.Value = 1;
            doubleAnimationUsingKeyFramesResult3.KeyFrames.Add(linearDoubleKeyFrameResult3);
            doubleAnimationUsingKeyFramesResult3.KeyFrames.Add(linearDoubleKeyFrameResult32);
            result3.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult3);


            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult4 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult4 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult42 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult4.KeyTime = TimeSpan.FromSeconds(0.6);
            linearDoubleKeyFrameResult4.Value = result4.Opacity;
            linearDoubleKeyFrameResult42.KeyTime = TimeSpan.FromSeconds(0.8);
            linearDoubleKeyFrameResult42.Value = 1;
            doubleAnimationUsingKeyFramesResult4.KeyFrames.Add(linearDoubleKeyFrameResult4);
            doubleAnimationUsingKeyFramesResult4.KeyFrames.Add(linearDoubleKeyFrameResult42);
            result4.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult4);


            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult5 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult5 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult52 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult5.KeyTime = TimeSpan.FromSeconds(0.8);
            linearDoubleKeyFrameResult5.Value = result5.Opacity;
            linearDoubleKeyFrameResult52.KeyTime = TimeSpan.FromSeconds(1);
            linearDoubleKeyFrameResult52.Value = 1;
            doubleAnimationUsingKeyFramesResult5.KeyFrames.Add(linearDoubleKeyFrameResult5);
            doubleAnimationUsingKeyFramesResult5.KeyFrames.Add(linearDoubleKeyFrameResult52);
            result5.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult5);

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult6 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult6 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult62 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult6.KeyTime = TimeSpan.FromSeconds(1);
            linearDoubleKeyFrameResult6.Value = result6.Opacity;
            linearDoubleKeyFrameResult62.KeyTime = TimeSpan.FromSeconds(1.2);
            linearDoubleKeyFrameResult62.Value = 1;
            doubleAnimationUsingKeyFramesResult6.KeyFrames.Add(linearDoubleKeyFrameResult6);
            doubleAnimationUsingKeyFramesResult6.KeyFrames.Add(linearDoubleKeyFrameResult62);
            result6.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult6);

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult7 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult7 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult72 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult7.KeyTime = TimeSpan.FromSeconds(1.2);
            linearDoubleKeyFrameResult7.Value = result7.Opacity;
            linearDoubleKeyFrameResult72.KeyTime = TimeSpan.FromSeconds(1.4);
            linearDoubleKeyFrameResult72.Value = 1;
            doubleAnimationUsingKeyFramesResult7.KeyFrames.Add(linearDoubleKeyFrameResult7);
            doubleAnimationUsingKeyFramesResult7.KeyFrames.Add(linearDoubleKeyFrameResult72);
            result7.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult7);

            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramesResult8 = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult8 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameResult82 = new LinearDoubleKeyFrame();
            linearDoubleKeyFrameResult8.KeyTime = TimeSpan.FromSeconds(1.4);
            linearDoubleKeyFrameResult8.Value = result8.Opacity;
            linearDoubleKeyFrameResult82.KeyTime = TimeSpan.FromSeconds(1.6);
            linearDoubleKeyFrameResult82.Value = 1;
            doubleAnimationUsingKeyFramesResult8.KeyFrames.Add(linearDoubleKeyFrameResult8);
            doubleAnimationUsingKeyFramesResult8.KeyFrames.Add(linearDoubleKeyFrameResult82);
            result8.BeginAnimation(StackPanel.OpacityProperty, doubleAnimationUsingKeyFramesResult8);

          
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFramessaveTest = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFramesaveTest = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFramesaveTest2 = new LinearDoubleKeyFrame();
            linearDoubleKeyFramesaveTest.KeyTime = TimeSpan.FromSeconds(1.8);
            linearDoubleKeyFramesaveTest.Value = 0;
            linearDoubleKeyFramesaveTest2.KeyTime = TimeSpan.FromSeconds(2);
            linearDoubleKeyFramesaveTest2.Value = 1;
            doubleAnimationUsingKeyFramessaveTest.KeyFrames.Add(linearDoubleKeyFramesaveTest);
            doubleAnimationUsingKeyFramessaveTest.KeyFrames.Add(linearDoubleKeyFramesaveTest2);
            saveTest.BeginAnimation(Button.OpacityProperty, doubleAnimationUsingKeyFramessaveTest);

            toResultsYourTest.Visibility = Visibility.Hidden;
        }

        private void saveTest_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            ind1.Background = new SolidColorBrush(Colors.Green);
            ind2.Background = new SolidColorBrush(Colors.Green);
            ind3.Background = new SolidColorBrush(Colors.Green);
            ind4.Background = new SolidColorBrush(Colors.Green);
            ind5.Background = new SolidColorBrush(Colors.Green);
            ind6.Background = new SolidColorBrush(Colors.Green);
            ind7.Background = new SolidColorBrush(Colors.Green);
            ind8.Background = new SolidColorBrush(Colors.Green);
            if (resultNameABC.Text == "") { flag = false; ind1.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameDBC.Text == "") { flag = false; ind2.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameAEC.Text == "") { flag = false; ind3.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameDEC.Text == "") { flag = false; ind4.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameAEF.Text == "") { flag = false; ind5.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameDEF.Text == "") { flag = false; ind6.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameABF.Text == "") { flag = false; ind7.Background = new SolidColorBrush(Colors.Red); };
            if (resultNameDBF.Text == "") { flag = false; ind8.Background = new SolidColorBrush(Colors.Red); };

            if (resultDescriptionABC.Text == "") { flag = false; ind1.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionDBC.Text == "") { flag = false; ind2.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionAEC.Text == "") { flag = false; ind3.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionDEC.Text == "") { flag = false; ind4.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionAEF.Text == "") { flag = false; ind5.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionDEF.Text == "") { flag = false; ind6.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionABF.Text == "") { flag = false; ind7.Background = new SolidColorBrush(Colors.Red); };
            if (resultDescriptionDBF.Text == "") { flag = false; ind8.Background = new SolidColorBrush(Colors.Red); };
            if (flag)
            {
                using (sixteenPersDB db = new sixteenPersDB())
                {
                    my_test MyTest = new my_test { test_name = currentMyTestName, host_login = CurrentUser.login, description = currentMyTestDescription };
                    db.my_test.Add(MyTest);
                    for (int i = 0; i < 11; i++)
                    {
                        question myQuestion = new question() { question_number = i, test_name = currentMyTestName, text = yourQuestionsText[i] };
                        db.questions.Add(myQuestion);
                    }
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameABC.Text, test_name = currentMyTestName, description = resultDescriptionABC.Text, type_descriptor = "ABC" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameDBC.Text, test_name = currentMyTestName, description = resultDescriptionDBC.Text, type_descriptor = "DBC" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameAEC.Text, test_name = currentMyTestName, description = resultDescriptionAEC.Text, type_descriptor = "AEC" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameDEC.Text, test_name = currentMyTestName, description = resultDescriptionDEC.Text, type_descriptor = "DEC" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameAEF.Text, test_name = currentMyTestName, description = resultDescriptionAEF.Text, type_descriptor = "AEF" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameDEF.Text, test_name = currentMyTestName, description = resultDescriptionDEF.Text, type_descriptor = "DEF" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameABF.Text, test_name = currentMyTestName, description = resultDescriptionABF.Text, type_descriptor = "ABF" });
                    db.my_test_decriptor.Add(new my_test_decriptor() { name_descriptor = resultNameDBF.Text, test_name = currentMyTestName, description = resultDescriptionDBF.Text, type_descriptor = "DBF" });
                    db.SaveChanges();
                    generateYourTestsBlocks();
                }
            }
        }

        public void generateYourTestsBlocks()
        {
            using (sixteenPersDB db = new sixteenPersDB())
            {
                var myTests = db.my_test.Where(p => p.host_login == CurrentUser.login);
                if (myTests.Count() == 0)
                {
                    beginCreatePanel.Visibility = Visibility.Visible;
                    DoubleAnimation beginCreatePanelAnimation = new DoubleAnimation();
                    beginCreatePanelAnimation.From = beginCreatePanel.Opacity;
                    beginCreatePanelAnimation.To = 1;
                    beginCreatePanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                    beginCreatePanel.BeginAnimation(StackPanel.OpacityProperty, beginCreatePanelAnimation);
                    MenuPanel.Visibility = Visibility.Hidden;
                }
                foreach (my_test mt in myTests)
                {
                        TextBlock testName = new TextBlock();
                        testName.Text = mt.test_name;
                        testName.Foreground = new SolidColorBrush(Colors.White);
                        testName.FontSize = 50 / db.my_test.Count();
                        testName.FontWeight = FontWeights.Bold;
                        testName.HorizontalAlignment = HorizontalAlignment.Center;
                        testName.VerticalAlignment = VerticalAlignment.Center;
                        Border testBlock = new Border();
                        testBlock.Width = 350 / db.my_test.Count();
                        testBlock.Height = 200 / db.my_test.Count();
                        testBlock.Background = new SolidColorBrush(Colors.MediumSeaGreen);
                        testBlock.Margin = new Thickness(5);
                        testBlock.CornerRadius = new CornerRadius(200 / db.my_test.Count() / 3);
                        testBlock.Child = testName;
                        testBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        testBlock.VerticalAlignment = VerticalAlignment.Center;
                        myTestsPanel.Children.Add(testBlock);
                        testBlock.MouseDown += myChosenTest_MouseDown;
                }
            }
         }

        private void myChosenTest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (sixteenPersDB db = new sixteenPersDB())
            {
                if (sender is Border)
                {
                    Border bord = (Border)sender;
                    if (bord.Child is TextBlock)
                    {
                        TextBlock text = (TextBlock)bord.Child;
                        myTestsPanel.Visibility = Visibility.Hidden;
                        yourTestOptionPanel.Visibility = Visibility.Visible;
                        currentMyTestName = text.Text;

                        var descr = from d in db.my_test.ToList()
                                    where d.test_name == currentMyTestName
                                    select d;
                        my_test MT = (my_test)descr.First();
                        yourTestDescriptionText.Text = MT.description;
                        DoubleAnimation yourTestOptionPanelAnimation = new DoubleAnimation();
                        yourTestOptionPanelAnimation.From = yourTestOptionPanel.Opacity;
                        yourTestOptionPanelAnimation.To = 1;
                        yourTestOptionPanelAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        yourTestOptionPanel.BeginAnimation(TextBox.OpacityProperty, yourTestOptionPanelAnimation);
                        myTestsPanel.Children.Clear();
                    }
                }
            }
        }

        private void persType_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Analytics.Visibility = Visibility.Hidden;
            Diplomats.Visibility = Visibility.Hidden;
            Guardians.Visibility = Visibility.Hidden;
            Crawlers.Visibility = Visibility.Hidden;

            infoPanel.Visibility = Visibility.Visible;
            infoPanel.Opacity = 1;
            if (image.Name == "Architect")
            {
                resultPers.Text = "Cтратег";
                resultDescriptionPers.Text = persDescriptions[0];
                ArchitectRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Logician")
            {
                resultPers.Text = "Учёный";
                resultDescriptionPers.Text = persDescriptions[1];
                LogicianRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Commander")
            {
                resultPers.Text = "Командир";
                resultDescriptionPers.Text = persDescriptions[2];
                CommanderRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Debater")
            {
                resultPers.Text = "Полемист";
                resultDescriptionPers.Text = persDescriptions[3];
                DebaterRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Advocate")
            {
                resultPers.Text = "Активист";
                resultDescriptionPers.Text = persDescriptions[4];
                AdvocateRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Mediator")
            {
                resultPers.Text = "Посредник";
                resultDescriptionPers.Text = persDescriptions[5];
                MediatorRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Protagonist")
            {
                resultPers.Text = "Протагонист";
                resultDescriptionPers.Text = persDescriptions[6];
                ProtagonistRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Campaigner")
            {
                resultPers.Text = "Протагонист";
                resultDescriptionPers.Text = persDescriptions[7];
                CampaignerRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Logistician")
            {
                resultPers.Text = "Администратор";
                resultDescriptionPers.Text = persDescriptions[8];
                LogisticianRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Defender")
            {
                resultPers.Text = "Защитник";
                resultDescriptionPers.Text = persDescriptions[9];
                DefenderRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Executive")
            {
                resultPers.Text = "Менеджер";
                resultDescriptionPers.Text = persDescriptions[10];
                ExecutiveRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Consul")
            {
                resultPers.Text = "Консул";
                resultDescriptionPers.Text = persDescriptions[11];
                ConsulRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Virtuoso")
            {
                resultPers.Text = "Виртуоз";
                resultDescriptionPers.Text = persDescriptions[12];
                VirtuosoRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Adventurer")
            {
                resultPers.Text = "Искатель";
                resultDescriptionPers.Text = persDescriptions[13];
                AdventurerRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Entrepreneur")
            {
                resultPers.Text = "Делец";
                resultDescriptionPers.Text = persDescriptions[14];
                EntrepreneurRes.Visibility = Visibility.Visible;
            }
            if (image.Name == "Enterteiner")
            {
                resultPers.Text = "Увеселитель";
                resultDescriptionPers.Text = persDescriptions[15];
                EnterteinerRes.Visibility = Visibility.Visible;
            }
        }

        private void buttonAnalytics_Click(object sender, RoutedEventArgs e)
        {
            fourPersGroups.Visibility = Visibility.Hidden;
            Analytics.Visibility = Visibility.Visible;
        }

        private void buttonGuardians_Click(object sender, RoutedEventArgs e)
        {
            fourPersGroups.Visibility = Visibility.Hidden;
            Guardians.Visibility = Visibility.Visible;
        }

        private void buttonCrawlers_Click(object sender, RoutedEventArgs e)
        {
            fourPersGroups.Visibility = Visibility.Hidden;
            Crawlers.Visibility = Visibility.Visible;
        }

        private void buttonDiplomats_Click(object sender, RoutedEventArgs e)
        {
            fourPersGroups.Visibility = Visibility.Hidden;
            Diplomats.Visibility = Visibility.Visible;
        }

        private void pers_MouseEnter(object sender, MouseEventArgs e)
        {
            Image imagePers = (Image)sender;
            if(imagePers.Name== "Architect")
            {
                DoubleAnimation ArchitectAnimation = new DoubleAnimation();
                ArchitectAnimation.From = Architect.Height;
                ArchitectAnimation.To = 330;
                ArchitectAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Architect.BeginAnimation(Image.HeightProperty, ArchitectAnimation);
            }
            if (imagePers.Name == "Logician")
            {
                DoubleAnimation LogicianAnimation = new DoubleAnimation();
                LogicianAnimation.From = Logician.Height;
                LogicianAnimation.To = 330;
                LogicianAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Logician.BeginAnimation(Image.HeightProperty, LogicianAnimation);
            }
            if (imagePers.Name == "Commander")
            {
                DoubleAnimation CommanderAnimation = new DoubleAnimation();
                CommanderAnimation.From = Commander.Height;
                CommanderAnimation.To = 330;
                CommanderAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Commander.BeginAnimation(Image.HeightProperty, CommanderAnimation);
            }
            if (imagePers.Name == "Debater")
            {
                DoubleAnimation DebaterAnimation = new DoubleAnimation();
                DebaterAnimation.From = Commander.Height;
                DebaterAnimation.To = 330;
                DebaterAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Debater.BeginAnimation(Image.HeightProperty, DebaterAnimation);
            }
            if (imagePers.Name == "Advocate")
            {
                DoubleAnimation AdvocateAnimation = new DoubleAnimation();
                AdvocateAnimation.From = Commander.Height;
                AdvocateAnimation.To = 330;
                AdvocateAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Advocate.BeginAnimation(Image.HeightProperty, AdvocateAnimation);
            }
            if (imagePers.Name == "Mediator")
            {
                DoubleAnimation MediatorAnimation = new DoubleAnimation();
                MediatorAnimation.From = Commander.Height;
                MediatorAnimation.To = 330;
                MediatorAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Mediator.BeginAnimation(Image.HeightProperty, MediatorAnimation);
            }
            if (imagePers.Name == "Protagonist")
            {
                DoubleAnimation ProtagonistAnimation = new DoubleAnimation();
                ProtagonistAnimation.From = Commander.Height;
                ProtagonistAnimation.To = 330;
                ProtagonistAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Protagonist.BeginAnimation(Image.HeightProperty, ProtagonistAnimation);
            }
            if (imagePers.Name == "Campaigner")
            {
                DoubleAnimation CampaignerAnimation = new DoubleAnimation();
                CampaignerAnimation.From = Commander.Height;
                CampaignerAnimation.To = 330;
                CampaignerAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Campaigner.BeginAnimation(Image.HeightProperty, CampaignerAnimation);
            }
            if (imagePers.Name == "Logistician")
            {
                DoubleAnimation LogisticianAnimation = new DoubleAnimation();
                LogisticianAnimation.From = Commander.Height;
                LogisticianAnimation.To = 330;
                LogisticianAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Logistician.BeginAnimation(Image.HeightProperty, LogisticianAnimation);
            }
            if (imagePers.Name == "Defender")
            {
                DoubleAnimation DefenderAnimation = new DoubleAnimation();
                DefenderAnimation.From = Commander.Height;
                DefenderAnimation.To = 330;
                DefenderAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Defender.BeginAnimation(Image.HeightProperty, DefenderAnimation);
            }
            if (imagePers.Name == "Executive")
            {
                DoubleAnimation ExecutiveAnimation = new DoubleAnimation();
                ExecutiveAnimation.From = Commander.Height;
                ExecutiveAnimation.To = 330;
                ExecutiveAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Executive.BeginAnimation(Image.HeightProperty, ExecutiveAnimation);
            }
            if (imagePers.Name == "Consul")
            {
                DoubleAnimation ConsulAnimation = new DoubleAnimation();
                ConsulAnimation.From = Commander.Height;
                ConsulAnimation.To = 330;
                ConsulAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Consul.BeginAnimation(Image.HeightProperty, ConsulAnimation);
            }
            if (imagePers.Name == "Consul")
            {
                DoubleAnimation ConsulAnimation = new DoubleAnimation();
                ConsulAnimation.From = Commander.Height;
                ConsulAnimation.To = 330;
                ConsulAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Consul.BeginAnimation(Image.HeightProperty, ConsulAnimation);
            }
            if (imagePers.Name == "Virtuoso")
            {
                DoubleAnimation VirtuosoAnimation = new DoubleAnimation();
                VirtuosoAnimation.From = Commander.Height;
                VirtuosoAnimation.To = 330;
                VirtuosoAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Virtuoso.BeginAnimation(Image.HeightProperty, VirtuosoAnimation);
            }
            if (imagePers.Name == "Adventurer")
            {
                DoubleAnimation AdventurerAnimation = new DoubleAnimation();
                AdventurerAnimation.From = Commander.Height;
                AdventurerAnimation.To = 330;
                AdventurerAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Adventurer.BeginAnimation(Image.HeightProperty, AdventurerAnimation);
            }
            if (imagePers.Name == "Entrepreneur")
            {
                DoubleAnimation EntrepreneurAnimation = new DoubleAnimation();
                EntrepreneurAnimation.From = Commander.Height;
                EntrepreneurAnimation.To = 330;
                EntrepreneurAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Entrepreneur.BeginAnimation(Image.HeightProperty, EntrepreneurAnimation);
            }
            if (imagePers.Name == "Enterteiner")
            {
                DoubleAnimation EnterteinerAnimation = new DoubleAnimation();
                EnterteinerAnimation.From = Commander.Height;
                EnterteinerAnimation.To = 330;
                EnterteinerAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Enterteiner.BeginAnimation(Image.HeightProperty, EnterteinerAnimation);
            }
        }

        private void pers_MouseLeave(object sender, MouseEventArgs e)
        {
            Image imagePers = (Image)sender;
            if (imagePers.Name == "Architect")
            {
                DoubleAnimation ArchitectAnimation = new DoubleAnimation();
                ArchitectAnimation.From = Architect.Height;
                ArchitectAnimation.To = 300;
                ArchitectAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Architect.BeginAnimation(Image.HeightProperty, ArchitectAnimation);
            }
            if (imagePers.Name == "Logician")
            {
                DoubleAnimation LogicianAnimation = new DoubleAnimation();
                LogicianAnimation.From = Logician.Height;
                LogicianAnimation.To = 300;
                LogicianAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Logician.BeginAnimation(Image.HeightProperty, LogicianAnimation);
            }
            if (imagePers.Name == "Commander")
            {
                DoubleAnimation CommanderAnimation = new DoubleAnimation();
                CommanderAnimation.From = Commander.Height;
                CommanderAnimation.To = 300;
                CommanderAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Commander.BeginAnimation(Image.HeightProperty, CommanderAnimation);
            }
            if (imagePers.Name == "Debater")
            {
                DoubleAnimation DebaterAnimation = new DoubleAnimation();
                DebaterAnimation.From = Commander.Height;
                DebaterAnimation.To = 300;
                DebaterAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Debater.BeginAnimation(Image.HeightProperty, DebaterAnimation);
            }
            if (imagePers.Name == "Advocate")
            {
                DoubleAnimation AdvocateAnimation = new DoubleAnimation();
                AdvocateAnimation.From = Commander.Height;
                AdvocateAnimation.To = 300;
                AdvocateAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Advocate.BeginAnimation(Image.HeightProperty, AdvocateAnimation);
            }
            if (imagePers.Name == "Mediator")
            {
                DoubleAnimation MediatorAnimation = new DoubleAnimation();
                MediatorAnimation.From = Commander.Height;
                MediatorAnimation.To = 300;
                MediatorAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Mediator.BeginAnimation(Image.HeightProperty, MediatorAnimation);
            }
            if (imagePers.Name == "Protagonist")
            {
                DoubleAnimation ProtagonistAnimation = new DoubleAnimation();
                ProtagonistAnimation.From = Commander.Height;
                ProtagonistAnimation.To = 300;
                ProtagonistAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Protagonist.BeginAnimation(Image.HeightProperty, ProtagonistAnimation);
            }
            if (imagePers.Name == "Campaigner")
            {
                DoubleAnimation CampaignerAnimation = new DoubleAnimation();
                CampaignerAnimation.From = Commander.Height;
                CampaignerAnimation.To = 300;
                CampaignerAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Campaigner.BeginAnimation(Image.HeightProperty, CampaignerAnimation);
            }
            if (imagePers.Name == "Logistician")
            {
                DoubleAnimation LogisticianAnimation = new DoubleAnimation();
                LogisticianAnimation.From = Commander.Height;
                LogisticianAnimation.To = 300;
                LogisticianAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Logistician.BeginAnimation(Image.HeightProperty, LogisticianAnimation);
            }
            if (imagePers.Name == "Defender")
            {
                DoubleAnimation DefenderAnimation = new DoubleAnimation();
                DefenderAnimation.From = Commander.Height;
                DefenderAnimation.To = 300;
                DefenderAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Defender.BeginAnimation(Image.HeightProperty, DefenderAnimation);
            }
            if (imagePers.Name == "Executive")
            {
                DoubleAnimation ExecutiveAnimation = new DoubleAnimation();
                ExecutiveAnimation.From = Commander.Height;
                ExecutiveAnimation.To = 300;
                ExecutiveAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Executive.BeginAnimation(Image.HeightProperty, ExecutiveAnimation);
            }
            if (imagePers.Name == "Consul")
            {
                DoubleAnimation ConsulAnimation = new DoubleAnimation();
                ConsulAnimation.From = Commander.Height;
                ConsulAnimation.To = 300;
                ConsulAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Consul.BeginAnimation(Image.HeightProperty, ConsulAnimation);
            }
            if (imagePers.Name == "Consul")
            {
                DoubleAnimation ConsulAnimation = new DoubleAnimation();
                ConsulAnimation.From = Commander.Height;
                ConsulAnimation.To = 300;
                ConsulAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Consul.BeginAnimation(Image.HeightProperty, ConsulAnimation);
            }
            if (imagePers.Name == "Virtuoso")
            {
                DoubleAnimation VirtuosoAnimation = new DoubleAnimation();
                VirtuosoAnimation.From = Commander.Height;
                VirtuosoAnimation.To = 300;
                VirtuosoAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Virtuoso.BeginAnimation(Image.HeightProperty, VirtuosoAnimation);
            }
            if (imagePers.Name == "Adventurer")
            {
                DoubleAnimation AdventurerAnimation = new DoubleAnimation();
                AdventurerAnimation.From = Commander.Height;
                AdventurerAnimation.To = 300;
                AdventurerAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Adventurer.BeginAnimation(Image.HeightProperty, AdventurerAnimation);
            }
            if (imagePers.Name == "Entrepreneur")
            {
                DoubleAnimation EntrepreneurAnimation = new DoubleAnimation();
                EntrepreneurAnimation.From = Commander.Height;
                EntrepreneurAnimation.To = 300;
                EntrepreneurAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Entrepreneur.BeginAnimation(Image.HeightProperty, EntrepreneurAnimation);
            }
            if (imagePers.Name == "Enterteiner")
            {
                DoubleAnimation EnterteinerAnimation = new DoubleAnimation();
                EnterteinerAnimation.From = Commander.Height;
                EnterteinerAnimation.To = 300;
                EnterteinerAnimation.Duration = TimeSpan.FromSeconds(0.3);
                Enterteiner.BeginAnimation(Image.HeightProperty, EnterteinerAnimation);
            }
        }
        public void opacityAnimateStackPanel(StackPanel st)
        {
            
            DoubleAnimation stAnimation = new DoubleAnimation();
            stAnimation.From = st.Opacity;
            stAnimation.To = 1;
            stAnimation.Duration = TimeSpan.FromSeconds(0.3);
            st.BeginAnimation(StackPanel.OpacityProperty, stAnimation);
        }
        public void opacityAnimateTextBlock(TextBlock tb)
        {

            DoubleAnimation tbAnimation = new DoubleAnimation();
            tbAnimation.From = tb.Opacity;
            tbAnimation.To = 1;
            tbAnimation.Duration = TimeSpan.FromSeconds(0.3);
            tb.BeginAnimation(TextBlock.OpacityProperty, tbAnimation);
        }
        public void opacityAnimateButton(Button bt)
        {

            DoubleAnimation btAnimation = new DoubleAnimation();
            btAnimation.From = bt.Opacity;
            btAnimation.To = 1;
            btAnimation.Duration = TimeSpan.FromSeconds(0.3);
            bt.BeginAnimation(Button.OpacityProperty, btAnimation);
        }
        public void opacityAnimateBorder(Border br)
        {

            DoubleAnimation brAnimation = new DoubleAnimation();
            brAnimation.From = br.Opacity;
            brAnimation.To = 1;
            brAnimation.Duration = TimeSpan.FromSeconds(0.3);
            br.BeginAnimation(Border.OpacityProperty, brAnimation);
        }
        private void buttonDeleteYourTest_Click(object sender, RoutedEventArgs e)
        {
            yourTestOptionPanel.Visibility=Visibility.Hidden;
            areYourSurePanel.Visibility = Visibility.Visible;
            opacityAnimateBorder(areYourSurePanel);
        }

        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            areYourSurePanel.Visibility = Visibility.Hidden;
            myTestsPanel.Visibility = Visibility.Visible;
            using (sixteenPersDB db = new sixteenPersDB()) 
            {
                foreach (my_test mt in db.my_test)
                {
                    if (mt.test_name == currentMyTestName)
                    {
                        db.my_test.Remove(mt);
                        
                    }
                }
                foreach(question qt in db.questions)
                {
                    if (qt.test_name == currentMyTestName)
                    {
                        db.questions.Remove(qt);
                       
                    }
                }
                foreach (my_test_decriptor des  in db.my_test_decriptor)
                {
                    if (des.test_name == currentMyTestName)
                    {
                        db.my_test_decriptor.Remove(des);

                    }
                }
                db.SaveChanges();
                generateYourTestsBlocks();
            }
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            areYourSurePanel.Visibility = Visibility.Hidden;
            yourTestOptionPanel.Visibility = Visibility.Visible;
        }

        private void buttonStartYourTest_Click(object sender, RoutedEventArgs e)
        {
            statusDynamicBar.Visibility = Visibility.Visible;
            opacityAnimateStackPanel(statusDynamicBar);
            buttonNextQuestionRunningCustomTest.Visibility = Visibility.Visible;
            opacityAnimateButton(buttonNextQuestionRunningCustomTest);
            foreach (Ellipse el in statusDynamicBar.Children)
            {
                el.Fill = new SolidColorBrush(Colors.Red);
            }
            question.Visibility = Visibility.Visible;
            opacityAnimateTextBlock(question);
            radioPanel.Visibility = Visibility.Visible;
            opacityAnimateStackPanel(radioPanel);
            using (sixteenPersDB db = new sixteenPersDB())
            {
                var qts = from q in db.questions.ToList()
                          where q.test_name == currentMyTestName
                          select q;

                yourQuestionsText.Clear();
                foreach (question q in qts)
                {
                    yourQuestionsText.Add(q.text);
                }
                question.Text = yourQuestionsText.First();
            }
        }
    }


}

