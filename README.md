# c-sharp-interview-challenges

## Задача 1
Есть ли в реализации класса какие-то проблемы и недостатки? Если есть,  то перечислите какие и предложите изменения:

```cs
public class Processor
{
    public void ProcessFile(string filename)
    {
        Stream fileStream = File.OpenRead(filename);
        Console.WriteLine(ReadAllContent(fileStream));
        fileStream.Close();
    }

    public string ReadAllContent(Stream stream)
    {
        StreamReader streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }
}
```

Ответ:	Нарушен принцип единственной ответственности (SOLID), т.к. класс решает две разные задачи — чтение и обработки данных. Отсюда проблемы с невозможностью переиспользования и тестирования данного кода. Возможно, чтение сразу всего содержимого файла, не лучшая идея - я бы подумал, исходя из задачи, либо над построчным чтением файла, либо использовал бы BufferStream. 
	В данном случае, я бы предложил разбить класс на два отдельных — Reader и Processor. Задача Reader – открытие и получения содержимого файла. Экземпляр Reader подается на вход объекту Processor. Задача Processor — обработка данных, поступающих от объекта Reader. 

## Задача 2
Будет ли работать реализация GetFileHandler в многопоточной среде. Поясните свой ответ

```cs
  public class FileHandler
    {
        public static Stream GetFileHandler()
        {
            if (_file!=null)
            	return _file;

            lock(_lock)
            {
                if (_file != null)
                    return _file;

                _file = File.OpenRead("name");

                return _file;
            }
        }

        private static object _lock = new object();
        private static Stream _file;
    }
```

Ответ: Объект Stream потоконебезопасен и в данном случае могут быть проблемы при одновременном обращении нескольких потоков к объекту Stream, возвращаемому методом GetFileHandler(). В зависимости от задачи, я бы предложил либо создать потокобезопасный объект Stream (метод Stream.Synchronized()), либо вообще не отдавать stream и подумать над реализацией потокобезопасной обертки, которая скроет его использование. Еще, не понятно как в данном решении предполагается считывать данные? Разные потоки могут перемещать каретку внутри stream, в результате потоки могут получать данные из разных частей файла и нарушится целостность считываемых данных. Плюс к этому — файл открывается на чтение, но из контекста это не понятно и возможно, вызывающий код захочет записать данные в stream и получит исключение, которое он может не ожидать.

## Задача 3
Есть класс, который занимается обработкой файлов разного формата. Предполагается, что будут добавляться новые форматы файлов. Какие вы видите проблемы в реализации.
Проанализируйте реализацию данного класса и предложите рефакторинг, позволяющий расширять форматы обрабатываемых файлов. 

```cs
  public class FileProcessor
    {
        enum FileType
        {
            Html,
  Text
        }

        public void ProcessFile(string fileName)
        {
            StreamReader fileStream = new StreamReader(File.OpenRead(fileName));
            string fileContent = fileStream.ReadToEnd();

            if (fileContent.IndexOf("<html") != -1)
                ProcessHtmlFile(fileContent);
            else ProcessTextFile(fileContent);

            ProcessFile(fileContent,fileContent.IndexOf("<html")!= -1?FileType.Html:FileType.Text);
            fileStream.Close();

        }

        private void ProcessFile(string content,  FileType fileType)
        {
            
            switch(fileType)
            {
                case FileType.Html:
                    ProcessHtmlFile(content);
                    break;
                case FileType.Text:
                    ProcessTextFile(content);
                    break;
                default:
                    throw new Exception("Unknown file format");
            }
        }

        private void ProcessHtmlFile(string content)
        {
            ....            
        }

        private void ProcessTextFile(string content)
        {
            ....
        }

    }
```

Ответ: Нарушен принцип единственной ответственности, класс решает разные задачи – чтение и обработка данных файла, из-за этого трудно поддерживать и невозможно протестировать данный код. Рефакторинг в приложении к заданию.

## Задача 4
Есть  клиент, который считывает сообщения из сети по TCP/IP. Сообщения представляют собой XML и бывают трех типов (A,B,C).Клиент занимается обработкой сообщений. В процессе обработки сообщений происходит их разбор и сохранение в постоянном файловом хранилище. Кроме того, подсчитывается статистика  по обработке сообщений. Предложите свой вариант дизайна реализации(достаточно показать идею).
Ответ: Для реализации данного клиента предлагаю разделить предметную область на следующие основные объекты:
-	PacketReader - объект, отвечающий за прием пакетов по сети и их передачу на обработку диспетчерую При приеме парсит пакет, использую объект PacketParser.
-	PacketDispatcher, его задача — получить пакет от Reader и параллельно отдать пакет на обработку PacketProcessor. Алгоритм работы следующий: диспетчер, получая пакет от Reader, ложит его в очередь и возвращает управление вызывающему коду. Поток, запущенный диспетчером, забирает пакет из очереди и вызывает необходимый метод объекта PacketProcessor. В зависимости от необходимой производительности, PacketDispatcher может порождать один или несколько потоков обработки, брать потоки из пула потоков или реализовывать алгоритм балансировки.
-	PacketProcessor, задача — выполнить необходимую логику обработки пакета (подсчет статистики и т. д.) и передать пакет на сохранение объекту PacketSaver.
-	PacketSaver, задача — сохранить пакет в файловом хранилище.

<img src="https://github.com/sasha-tlt/c-sharp-interview-challenges/blob/main/Diagram.png?raw=true"/>


## Задача 5
Напишите метод, позволяющий посчитать количество элементов в коллекции, удовлетворяющих предикату. Предикат должен передаваться функции параметром.

## Задача 6
Напишите метод, расширяющий BitArray. Метод должен инициализировать созданный объект BitArray целым числом (int).
