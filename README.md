# OtelBudur

![Screenshot 2023-08-02 235825](https://github.com/ahmetyazbulursun/otelbudur/assets/88562237/7ce10119-6c88-41fe-939d-39688b319100)

- ![Static Badge](https://img.shields.io/badge/v1.0-1DA1F2)
- ![Static Badge](https://img.shields.io/badge/EN-0388fc)

## Developers

- [@ahmetyazbulursun](https://www.github.com/ahmetyazbulursun)


## Technologies

**Client:** HTML, CSS, JavaScript, Bootstrap

**Server:** .NET Core, C#, MSSQL, .NET Core Identity, N Tier Architecture, Web API, Mail Services, .NET Core Areas, Forgot Password


## Features

- User Registration and Login
- Mobile Device Compatibility
- Don't Forget the Password and Reset the Password
- Scheduled Jobs
- API Access
- E-Mail Verification and Mail Services
## Documentation

OtelBudur, a hotel reservation application developed with Core 5.0, in which a hotel reservation scenario is performed in a basic way.

A room search can be made with Location, Check-in-Out Date and Adult-Child number information on the main page of the application, and a question record can be created in the frequently asked questions field about a topic of interest. Support can be obtained on any subject by filling out the form on the contact page.

After the reservation search process, the rooms that meet the relevant criteria will be listed. Dec. From these rooms, by clicking on the desired room, the details of the room, room images and comments made to the room can be seen. When the reservation button is clicked on the page, the reservation process will be performed.

A user account with the 'Member' role must be created in order to perform the booking process. To create a member account, click on the Subscribe button located in the redirect area of the page.

On this page that opens, if there is a user account, you can log in, otherwise a new user account must be created. To create a user account, click on the 'Create an account' link. A user account will be created when the full name, username, Email address, phone number and password information are entered in accordance with the criteria on the page that opens. The reservation process can be performed by logging in to the system with the created account.

Members who have logged in to the system can access the profile panel where they can update their specific settings such as user information, profile photo and password, and see all their pending, approved, rejected and completed reservations.

When the room is transferred from the detail page to the booking confirmation page, it is redirected to the last browsing page where the daily price of the room and the total amount account will be Decoupled based on the time between the check-in and check-out date. After making a confirmation on this page, a status notification mail will be sent to the user's Email address about the reservation status.

Another role found in the application is the role of 'Hotel Owner'. The hotel owner, according to the hotel he is affiliated with, can view and intervene in booking requests received from this hotel, hotel detail information and information about the rooms belonging to the hotel.

The booking transactions performed will be located on the page of the user of each hotel who is connected to that hotel.

Booking transactions made to the relevant hotel will fall to the hotel owner management panel. Here, if the hotel owner confirms the reservation, the relevant reservation is initiated, and if he refuses, the reservation is canceled. When the reservation is confirmed, the reserved status of the room belonging to the reservation is activated until the check-out date. This room will not appear in booking searches until the reserved status is inactive.

In order to log in to the system as a hotel owner, the hotel owner login page can be accessed by clicking on the 'Log in as a Hotel Owner' link located in the footer area of the site. On this page, if there is a hotel owner account, the login process is performed, if there is no hotel owner account, the process of filling out the Hotel Owner request form is performed.

After entering the full name, username, Email address, phone number, location and hotel name information in the hotel owner request form, this entered information will be listed in the panel of the user with the 'Admin' role, who has full authority in the system.

Users who have the 'admin' role can interfere with the entire application. About me page, social media accounts, users, roles, mail transactions, hotels, rooms...

If the Hotel Owner's request is approved by the Admin, the user is created according to the user information in the request, and the hotel is created according to the hotel information in the request. When the request is approved, mail will be sent to the user's email address to determine his password. The user can determine the password on the system by clicking on the link in this email, and then use the system with a 'Hotel Owner' authorization.
## Feedback

About the issues you are curious about aheroglu@outlook.com you can contact me at the address.







# OtelBudur
- ![Static Badge](https://img.shields.io/badge/v1.0-1DA1F2)
- ![Static Badge](https://img.shields.io/badge/TR-ff1e00)
## Demo

![Demo](images/octocat.png)

## Geliştiriciler

- [@ahmetyazbulursun](https://www.github.com/ahmetyazbulursun)


## Teknolojiler

**İstemci:** HTML, CSS, JavaScript, Bootstrap

**Sunucu:** .NET Core, C#, MSSQL, .NET Core Identity, N Tier Architecture, Web API, Mail Services, .NET Core Areas, Forgot Password


## Özellikler

- Kullanıcı Kaydı ve Girişi
- Mobil Cihaz Uyumu
- Şifre Unutma ve Şifre Sıfırlama
- Zamanlanmış İşler
- API Erişimi
- E-Posta Doğrulama ve Mail Hizmetleri
## Dökümantasyon

OtelBudur Asp.Net Core 5.0 ile geliştirilmiş, bir otel rezervasyon senaryosunun temel şekilde gerçekleştirildiği otel rezervasyon uygulamasıdır.

Uygulamanın ana sayfasında Lokasyon, Giriş-Çıkış Tarih ve Yetişkin-Çocuk sayısı bilgileri ile oda araması yapılabilir, merak edilen bir konu hakkında sıkça sorulan sorular alanında soru kaydı oluşturulabilir. İletişim sayfasındaki form doldurularak herhangi bir konuda destek alınabilir.

Rezervasyon arama işlemi sonrasında ilgili kriterlere uygun odalar listelenecektir. Bu odalardan istenilen odaya tıklayarak odanın detayları, oda görselleri ve odaya yapılan yorumlar görülebilir. Sayfadaki rezervasyon butonuna tıklandığında rezervasyon işlemi gerçekleştirilecektir.

Rezervasyon işlemin gerçekleştirebilmek için 'Üye' rolüne sahip bir kullanıcı hesabı oluşturulmalıdır. Bir üye hesabı oluşturmak için sayfanın yönlendirme alanında yer alan Üye Ol butonuna tıklanmalıdır.

Açılan bu sayfada eğer ki kullanıcı hesabı var ise giriş yapılabilir, yoksa yeni bir kullanıcı hesabı oluşturulmalıdır. Kullanıcı hesabı oluşturmak için 'Bir hesap oluştur' linkine tıklanmalıdır. Açılan sayfada tam ad, kullanıcı adı, Email adresi, telefon numarası ve şifre bilgileri kriterlere uygun bir şekilde girildiğinde kullanıcı hesabı oluşturulacaktır. Oluşturulan hesap ile sisteme giriş yapılarak rezervasyon işlemi gerçekleştirilebilir.

Sisteme giriş yapan üyeler, profil paneline ulaşarak burada kullanıcı bilgileri, profil fotoğrafı ve şifre gibi kendine özel ayarları güncelleyebilir, hazırda bekleyen, onaylanan, reddedilen ve tamamlanan bütün rezervasyonlarını görebilir.

Oda detay sayfasından rezervasyon onay sayfasına aktarıldığında burada giriş-çıkış tarihi arasındaki zaman baz alınarak odanın günlük ücreti ile bir toplam tutar hesabı çıkarılacak son göz atma sayfasına yönlendirilir. Bu sayfada onay verdikten sonra rezervasyon durumu hakkında kullanıcının Email adresine bir durum bildirim postası gelecektir.

Uygulamada bulunan diğer bir rol ise 'Otel Sahibi' rolüdür. Otel sahibi, bağlı olduğu otele göre, bu otele gelen rezervasyon istekleri, otel detay bilgileri ve otele ait olan odalara ait bilgileri görüntüleyebilir, müdahele edebilir.

Gerçekleştirilen rezervasyon işlemleri, her otelin o otele bağlı olan kullanıcısının sayfasında yer alacaktır.

İlgili otele yapılan rezervasyon işlemleri, otel sahibi yönetim paneline düşecektir. Burada otel sahibi, rezervasyonu onaylarsa ilgili rezervasyon başlatılır, reddederse rezervasyon iptal olur. Rezervasyon onaylandığında çıkış tarihine kadar rezervasyona ait odanın rezerve durumu aktif olur. Bu oda, rezerve durumu pasif olana kadar rezervasyon aramalarında görünmeyecektir.

Sisteme otel sahibi olarak giriş yapabilmek için sitenin footer alanında yer alan 'Otel Sahibi olarak giriş yap' linkine tıklanarak otel sahibi giriş sayfasına erişilebilir. Bu sayfada eğer bir otel sahibi hesabı var ise giriş yapma işlemi, eğer bir otel sahibi hesabı yok ise Otel Sahibi isteği formu doldurma işlemi gerçekleştirilir.

Otel sahibi isteği formunda tam ad, kullanıcı adı, Email adresi, telefon numarası, lokasyon ve otel adı bilgileri girildikten sonra, girilen bu bilgiler sistemde tam yetkisi olan 'Admin' rolüne sahip kullanıcının panelinde listelenecektir.

'Admin' rolüne sahip olan kullanıcılar, uygulamanın tamamına müdahele edebilir. Hakkımda sayfası, sosyal medya hesapları, kullanıcılar, roller, mail işlemleri, oteller, odalar...

Otel Sahibi isteği, Admin tarafında onaylanırsa istekteki kullanıcı bilgilerine göre kullanıcı oluşturulur, istekteki otel bilgilerine göre otel oluşturulur. İstek onaylandığında kullanıcı email adresine parolasını belirlemesi için posta gönderilecektir. Kullanıcı bu email'deki linke tıklayarak sistemdeki parolasını belirleyebilir ve ardından sistemi bir 'Otel Sahibi' yetkisi ile kullanabilir.
## Geri Bildirim

Merak ettiğiniz konular hakkında aheroglu@outlook.com adresinden bana ulaşabilirsiniz.
