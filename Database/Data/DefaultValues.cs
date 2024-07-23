using Application.Models;

namespace Database.Data;

public class DefaultValues
{
	public static Note[] Notes()
	{
		Note[] notes =
		{
			new()
			{
				FirstName = "Иван",
				SecondName = "Иванов",
				ThirdName = "Иванович",
				PhoneNumber = "555-0100",
				Address = "ул. Ленина, д. 1",
				Description = "Друг из школы"
			},
			new()
			{
				FirstName = "Мария",
				SecondName = "Петрова",
				ThirdName = "Сергеевна",
				PhoneNumber = "555-0101",
				Address = "ул. Пушкина, д. 2",
				Description = "Коллега по работе"
			},
			new()
			{
				FirstName = "Алексей",
				SecondName = "Сидоров",
				ThirdName = "Николаевич",
				PhoneNumber = "555-0102",
				Address = "ул. Мира, д. 3",
				Description = "Спортивный друг"
			},
			new()
			{
				FirstName = "Екатерина",
				SecondName = "Смирнова",
				ThirdName = "Алексеевна",
				PhoneNumber = "555-0103",
				Address = "ул. Гоголя, д. 4",
				Description = "Соседка"
			},
			new()
			{
				FirstName = "Дмитрий",
				SecondName = "Кузнецов",
				ThirdName = "Владимирович",
				PhoneNumber = "555-0104",
				Address = "ул. Чехова, д. 5",
				Description = "Секретный агент"
			},
			new()
			{
				FirstName = "Ольга",
				SecondName = "Попова",
				ThirdName = "Игоревна",
				PhoneNumber = "555-0105",
				Address = "ул. Лермонтова, д. 6",
				Description = "Актриса"
			},
			new()
			{
				FirstName = "Сергей",
				SecondName = "Васильев",
				ThirdName = "Петрович",
				PhoneNumber = "555-0106",
				Address = "ул. Толстого, д. 7",
				Description = "Друзья со школы"
			},
			new()
			{
				FirstName = "Анна",
				SecondName = "Зайцева",
				ThirdName = "Максимовна",
				PhoneNumber = "555-0107",
				Address = "ул. Достоевского, д. 8",
				Description = "Подруга"
			},
			new()
			{
				FirstName = "Юрий",
				SecondName = "Морозов",
				ThirdName = "Олегович",
				PhoneNumber = "555-0108",
				Address = "ул. Тургенева, д. 9",
				Description = "Сосед"
			},
			new()
			{
				FirstName = "Татьяна",
				SecondName = "Волкова",
				ThirdName = "Андреевна",
				PhoneNumber = "555-0109",
				Address = "ул. Некрасова, д. 10",
				Description = "Подруга детства"
			},
			new()
			{
				FirstName = "Николай",
				SecondName = "Зуев",
				ThirdName = "Федорович",
				PhoneNumber = "555-0110",
				Address = "ул. Маяковского, д. 11",
				Description = "Бизнес-партнер"
			},
			new()
			{
				FirstName = "Светлана",
				SecondName = "Соколова",
				ThirdName = "Романовна",
				PhoneNumber = "555-0111",
				Address = "ул. Есенина, д. 12",
				Description = "Коллега"
			},
			new()
			{
				FirstName = "Владимир",
				SecondName = "Новиков",
				ThirdName = "Григорьевич",
				PhoneNumber = "555-0112",
				Address = "ул. Шолохова, д. 13",
				Description = "Друг детства"
			},
			new()
			{
				FirstName = "Елена",
				SecondName = "Федорова",
				ThirdName = "Павловна",
				PhoneNumber = "555-0113",
				Address = "ул. Грибоедова, д. 14",
				Description = "Соседка"
			},
			new()
			{
				FirstName = "Андрей",
				SecondName = "Михайлов",
				ThirdName = "Ильич",
				PhoneNumber = "555-0114",
				Address = "ул. Островского, д. 15",
				Description = "Коллега"
			},
			new()
			{
				FirstName = "Ирина",
				SecondName = "Богданова",
				ThirdName = "Юрьевна",
				PhoneNumber = "555-0115",
				Address = "ул. Блока, д. 16",
				Description = "Подруга"
			},
			new()
			{
				FirstName = "Максим",
				SecondName = "Гусев",
				ThirdName = "Борисович",
				PhoneNumber = "555-0116",
				Address = "ул. Ахматовой, д. 17",
				Description = "Друг"
			},
			new()
			{
				FirstName = "Виктория",
				SecondName = "Семенова",
				ThirdName = "Анатольевна",
				PhoneNumber = "555-0117",
				Address = "ул. Цветаевой, д. 18",
				Description = "Соседка"
			},
			new()
			{
				FirstName = "Александр",
				SecondName = "Орлов",
				ThirdName = "Викторович",
				PhoneNumber = "555-0118",
				Address = "ул. Бунина, д. 19",
				Description = "Коллега"
			},
			new()
			{
				FirstName = "Евгения",
				SecondName = "Алексеева",
				ThirdName = "Константиновна",
				PhoneNumber = "555-0119",
				Address = "ул. Твардовского, д. 20",
				Description = "Подруга"
			}
		};

		return notes;
	}

	public static User Admin = new()
	{
		UserName = "Admin",
		Email = "admin@example.com",
		PhoneNumber = "+7 900 555-66-77",
		PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
	};

	public static User User = new()
	{
		UserName = "User",
		Email = "johndoe@example.com",
		PhoneNumber = "+7 900 222-11-33",
		PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
	};

	public static Role AdminRole = new()
	{
		Name = "Admin", 
		Permissions = new []
		{
			Permission.CanSeeNotes,
			Permission.CanEditNote,
			Permission.CanCreateNewNote,
			Permission.CanDeleteNote,
			
			Permission.CanSeeUsers,
			Permission.CanEditUser,
			Permission.CanCreateNewUser,
			Permission.CanDeleteUser
		}
	};
	
	public static Role UserRole = new()
	{
		Name = "Admin", 
		Permissions = new []
		{
			Permission.CanSeeNotes,
			Permission.CanEditNote,
			Permission.CanCreateNewNote,
			Permission.CanDeleteNote,
		}
	};
}
