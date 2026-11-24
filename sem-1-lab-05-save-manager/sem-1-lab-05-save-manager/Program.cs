using Newtonsoft.Json;

namespace sem_1_lab_05_save_manager
{
    public class Program
    {
        static void Main(string[] args)
        {
            string jsonFile = "index.json";
            string saveFile = "saves";
            string backupsFile = "backups";

            List<SaveEntry> entries = LoadIndex(jsonFile);
            if (entries == null)
            {
                return;
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("1. Показать список сохранений");
                    Console.WriteLine("2. Посмотреть данные сохранения");
                    Console.WriteLine("3. Изменить данные персонажа и записать их обратно");
                    Console.WriteLine("4. Создать резервную копию");
                    Console.WriteLine("5. Восстановить резервную копию");
                    Console.WriteLine("6. Удалить сохранение");
                    Console.WriteLine("7. Выйти");
                    Console.WriteLine();
                    string choice = Console.ReadLine();
                    Console.WriteLine();
                    switch (choice)
                    {
                        case "1":

                            for (int i = 0; i < entries.Count; i++)
                            {
                                Console.WriteLine(entries[i].Name);
                            }
                            Console.WriteLine();
                            break;

                        case "2":
                            Console.WriteLine("введите сохранение, которое желаете просмотреть");
                            string typeSlot = Console.ReadLine();
                            SaveEntry slot = FindByName(entries, typeSlot);
                            if (slot != null)
                            {
                                SaveData showSave = LoadSave(saveFile, slot);
                                if (showSave == null)
                                {
                                    break;
                                }
                                else
                                {
                                    PrintSave(slot, showSave);
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Сохранение не найдено");
                                break;
                            }

                        case "3":
                            Console.WriteLine("введите сохранение, которое желаете изменить");
                            string typeSlotChange = Console.ReadLine();
                            SaveEntry slotChange = FindByName(entries, typeSlotChange);
                            if (slotChange != null)
                            {
                                SaveData showSave = LoadSave(saveFile, slotChange);
                                if (showSave == null)
                                {
                                    break;
                                }
                                else
                                {
                                    bool editing = true;
                                    while (editing)
                                    {
                                        Console.WriteLine("Выберите, что хотите изменить:");
                                        Console.WriteLine("1. Карта");
                                        Console.WriteLine("2. Координаты");
                                        Console.WriteLine("3. Здоровье");
                                        Console.WriteLine("4. Инвентарь");
                                        Console.WriteLine("5. Сохранить и вернуться в меню");
                                        Console.WriteLine();
                                        string editThis = Console.ReadLine();
                                        switch (editThis)
                                        {
                                            case "1":
                                                Console.WriteLine("текущая карта: " + showSave.Map);
                                                Console.WriteLine();
                                                Console.WriteLine("Введите новую карту");
                                                string newMap = Console.ReadLine();
                                                Console.WriteLine();
                                                if (newMap != "")
                                                {
                                                    showSave.Map = newMap;
                                                    Console.WriteLine("новая карта: " + showSave.Map);
                                                    Console.WriteLine();
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }

                                            case "2":
                                                Console.WriteLine($"текущие координаты: {showSave.Position.X} {showSave.Position.Y}");
                                                Console.WriteLine();
                                                Console.WriteLine("Введите X: ");
                                                string newPositionX = Console.ReadLine();
                                                if (newPositionX != "")
                                                {
                                                    if (int.TryParse(newPositionX, out int number))
                                                    {
                                                        showSave.Position.X = number;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Это не число");
                                                    }
                                                }
                                                
                                                Console.WriteLine();
                                                Console.WriteLine("Введите Y: ");
                                                string newPositionY = Console.ReadLine();
                                                if (newPositionY != "")
                                                {
                                                    if (int.TryParse(newPositionY, out int number))
                                                    {
                                                        showSave.Position.Y = number;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Это не число");
                                                    }
                                                }
                                                Console.WriteLine();
                                                Console.WriteLine($"новые координаты: {showSave.Position.X} {showSave.Position.Y}");
                                                break;

                                            case "3":
                                                Console.WriteLine("текущее здоровье: " + showSave.Health);
                                                Console.WriteLine();
                                                Console.WriteLine("Введите новое здоровье");
                                                string newHealth = Console.ReadLine();
                                                if (newHealth != "")
                                                {
                                                    if (int.TryParse(newHealth, out int isNumber))
                                                    {
                                                        showSave.Health = isNumber;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Это не число");
                                                    }
                                                }
                                                Console.WriteLine($"новое здоровье: {showSave.Health}");
                                                Console.WriteLine();
                                                break;

                                            case "4":
                                                Console.WriteLine("текущие предметы в инвенторе: " + string.Join(", ", showSave.Inventory));
                                                Console.WriteLine();
                                                bool editingItems = true;
                                                while (editingItems)
                                                {
                                                    Console.WriteLine("1. Добавить");
                                                    Console.WriteLine("2. Удалить");
                                                    Console.WriteLine("3. Готово");
                                                    Console.WriteLine();
                                                    string editThisItem = Console.ReadLine();
                                                    switch (editThisItem)
                                                    {
                                                        case "1":
                                                            Console.WriteLine("Введите предмет для добавления: ");
                                                            string addItem = Console.ReadLine();
                                                            if (addItem != "")
                                                            {
                                                                showSave.Inventory.Add(addItem);
                                                            }
                                                            Console.WriteLine();
                                                            break;

                                                        case "2":
                                                            Console.WriteLine("Введите предмет для удаления: ");
                                                            string removeItem = Console.ReadLine();
                                                            if (showSave.Inventory.Remove(removeItem) == true)
                                                            {
                                                                Console.WriteLine("Предмет удален");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Такого предмета нет");
                                                            }
                                                            Console.WriteLine();
                                                            break;

                                                        case "3":
                                                            Console.WriteLine("новые предметы в инвенторе: " + string.Join(", ", showSave.Inventory));
                                                            editingItems = false;
                                                            Console.WriteLine();
                                                            break;
                                                        default:
                                                            Console.WriteLine("Нет такого пункта");
                                                            Console.WriteLine();
                                                            break;
                                                    }
                                                }
                                                break;

                                            case "5":
                                                slotChange.UpdatedAt = DateTime.Now;
                                                SaveGame(saveFile, slotChange, showSave);
                                                SaveIndex(jsonFile, entries);
                                                Console.WriteLine("Сохранение обновлено");
                                                Console.WriteLine();
                                                editing = false;
                                                break;

                                            default:
                                                Console.WriteLine("Нет такого пункта");
                                                Console.WriteLine();
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Сохранение не найдено");
                                Console.WriteLine();
                                break;
                            }
                            break;

                        case "4":
                            Console.WriteLine("введите сохранение для резервной копии");
                            string typeSlotReserve = Console.ReadLine();
                            Console.WriteLine();
                            SaveEntry slotReserve = FindByName(entries, typeSlotReserve);
                            if (slotReserve != null)
                            {
                                CreateBackup(saveFile, backupsFile, slotReserve);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Сохранение не найдено");
                                Console.WriteLine();
                                break;
                            }
                            break;

                        case "5":
                            Console.WriteLine("введите сохранение, которое хотите восстановить");
                            string typeSlotRegen = Console.ReadLine();
                            SaveEntry slotRegen = FindByName(entries, typeSlotRegen);
                            if (slotRegen != null)
                            {
                                RestoreBackup(saveFile, backupsFile, slotRegen);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Сохранение не найдено");
                                Console.WriteLine();
                                break;
                            }
                            break;

                        case "6":
                            Console.WriteLine("введите сохранение, которое желаете удалить");
                            string typeSlotDelete = Console.ReadLine();
                            SaveEntry slotDelete = FindByName(entries, typeSlotDelete);
                            if (slotDelete != null)
                            {
                                DeleteSave(saveFile, jsonFile, entries, slotDelete);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Сохранение не найдено");
                                Console.WriteLine();
                                break;
                            }
                            break;

                        case "7":
                            return;

                        default:
                            Console.WriteLine("Нет такого пункта");
                            break;
                    }

                }
            }
        }

        public static List<SaveEntry> LoadIndex(string indexPath)
        {
            if (File.Exists(indexPath) == true)
            {
                try
                {
                    string json = File.ReadAllText(indexPath);
                    List<SaveEntry> entries = JsonConvert.DeserializeObject<List<SaveEntry>>(json);
                    return entries;
                }
                catch (JsonException)
                {
                    Console.WriteLine("Файл поврежден, чтение остановлено");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Файла не найден по заданному пути, проверьте имя и путь файла");
                return null;
            }
        }

        public static SaveEntry FindByName(List<SaveEntry> entries, string name)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                string lowName = name.ToLower();
                string entriesLowName = entries[i].Name.ToLower();
                if (entriesLowName == lowName)
                {
                    return entries[i];
                }
            }
            return null;
        }

        public static SaveData LoadSave(string savesDirectory, SaveEntry entry)
        {
            string filePath = Path.Combine(savesDirectory, entry.FileName);
            if (File.Exists(filePath) == true)
            {
                try
                {
                    string newSaveData = File.ReadAllText(filePath);
                    SaveData newSaveJsonData = JsonConvert.DeserializeObject<SaveData>(newSaveData);
                    return newSaveJsonData;
                }
                catch (JsonException)
                {
                    Console.WriteLine("Файл поврежден");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("файл не был обнаружен по заданному пути");
                return null;
            }
        }

        public static void PrintSave(SaveEntry entry, SaveData save)
        {
            int width = 54;
            string cordX = save.Position.X.ToString();
            string cordY = save.Position.Y.ToString();
            Console.WriteLine("|-------------------------------------------------------|");
            Console.WriteLine($"| {"Имя сохранения:",-18}{entry.Name,-36}|");
            Console.WriteLine($"| {"",54}|");
            Console.WriteLine($"| {"Имя персонажа".PadLeft("Имя персонажа".Length + (width - "Имя персонажа".Length) / 2).PadRight(width)}|");
            Console.WriteLine($"| {save.Character.PadLeft(save.Character.Length + (width - save.Character.Length) / 2).PadRight(width)}|");
            Console.WriteLine($"| {"",54}|");
            Console.WriteLine($"| {"Здоровье:",-12} {save.Health,-41}|");
            Console.WriteLine($"| {"Локация:",-11} {save.Map,-42}|");
            Console.WriteLine($"| {"Координаты:",-13} X = {cordX,-3} Y = {cordY,-28}|");
            Console.WriteLine($"| {"Предметы:",-11} {string.Join(", ", save.Inventory),-42}|");
            Console.WriteLine("|-------------------------------------------------------|");
            Console.WriteLine();
        }

        public static void SaveGame(string savesDirectory, SaveEntry entry, SaveData save)
        {
            string filePath = Path.Combine(savesDirectory, entry.FileName);
            string newSaveData = JsonConvert.SerializeObject(save, Formatting.Indented);
            File.WriteAllText(filePath, newSaveData);
        }

        public static void SaveIndex(string indexPath, List<SaveEntry> entries)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
            string newIndex = JsonConvert.SerializeObject(entries, settings);
            File.WriteAllText(indexPath, newIndex);
        }

        public static void CreateBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            string filePath = Path.Combine(savesDirectory, entry.FileName);
            if (File.Exists(filePath))
            {
                string backupFilePath = Path.Combine(backupsDirectory, entry.FileName);
                Directory.CreateDirectory(backupsDirectory);
                File.Copy(filePath, backupFilePath, true);
                Console.WriteLine("Резервная копия создана успешно!");
            }
            else
            {
                Console.WriteLine("Сохранение не найдено");
            }
        }

        public static void RestoreBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            string filePath = Path.Combine(savesDirectory, entry.FileName);
            string backupFilePath = Path.Combine(backupsDirectory, entry.FileName);
            if (File.Exists(backupFilePath))
            {
                Directory.CreateDirectory(savesDirectory);
                File.Copy(backupFilePath, filePath, true);
                Console.WriteLine("Сохранение восстановлено успешно!");
            }
            else
            {
                Console.WriteLine("Копия сохранения не найдена");
            }
        }
        public static void DeleteSave(string savesDirectory, string indexPath, List<SaveEntry> entries, SaveEntry entry)
        {
            string filePath = Path.Combine(savesDirectory, entry.FileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("Сохранение удалено успешно!");
            }
            else
            {
                Console.WriteLine("сохранение не было найдено");
            }
            entries.Remove(entry);
            SaveIndex(indexPath, entries);
        }
    }
}