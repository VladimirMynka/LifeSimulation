﻿## Что-то типа документации. Этап 4

### Содержание
1. [Основная структура классов](#основная-структура-классов)
2. [Компоненты](#компоненты)
   1. [Общие поля и методы](#общие-поля-и-методы)
   2. [Жизненный цикл и события компонента](#жизненный-цикл-и-события-компонента)
   3. [Обобщения](#обобщения)
   4. [Список существующих компонентов и их назначения](#список-существующих-компонентов-и-их-назначения)
3. [Мировые объекты](#мировые-объекты)
    1. [Конструктор и фабричные методы](#конструктор-и-фабричные-методы)
    2. [Список существующих объектов](#список-существующих-объектов)
___
### Основная структура классов
___
### Компоненты
Компонентами в данном проекте называются наследники
класса _`WorldObjectComponent`_. Компоненты содержат основную логику 
объектов мира, их различные комбинации позволяют сконструировать
объекты с совершенно разными свойствами.  

Для удобства, в названиях классов-компонентов 
пишется суффикс _Component_.  

Компоненты можно добавлять не только в момент создания объекта,
но и после этого. Это даёт возможности для дополнительной динамики.
Утрируя: мы можем создать компонент, который манипулирует другими
компонентами животного таким образом, что в итоге оно становится
зданием.  

Однако нельзя забывать, что одни компоненты могут зависеть от других,
и удаление первых приведёт к ошибкам в работе вторых.

Другое важное замечание: при использовании компонент очень важно 
проверять, не уничтожены ли они, и своевременно удалять ссылки на
них. Иначе вы не только будете использовать несуществующий компонент,
но и не дадите сборщику мусора в действительности удалить объект. 
Это может привести к многочисленным ошибкам типа 
_NullReferenceException_, причину которых сложно обнаружить через
Debug, так как вызываются они из других классов.

Для проверки следует использовать функцию

```C#
WorldObjectComponent.CheckWereDestroyed(component)
```

Она возвращает _`true`_, когда либо `component`,
либо `component.WorldObject`, либо `component.WorldObject.Cell` имеют
значение _`null`_.
<br/>
<br/>

#### Общие поля и методы
Все наследники _`WorldObjectComponent`_ имеют ссылку на экзмепляр 
_`WorldObject`_, содержащего их, и на _`World`_. Они также могут 
переопределять методы _`Start()`_, _`Update()`_, _`OnDestroy()`_, 
о которых будет сказано далее, использовать методы:

```c#
protected T GetComponent<T>() where T : WorldObjectComponent 
               //возвращает первый компонент типа T, содержащийся 
               //в WorldObject текущего компонента, или null
protected List<T> GetComponents<T>()
               //возвращает список всех компонентов данного типа.
               //например, реализующих некоторый интерфейс
public void CheckWereDestroyed(WorldObject worldObject) //проверяет, уничтожен ли этот объект
public void CheckWereDestroyed(WorldObjectComponent component) //проверяет, уничтожен ли этот компонент
protected void Destroy() //уничтожает текущий компонент с вызовом OnDestroy()
```

В некоторых случаях необходимо удалить компонент не вызывая 
_`OnDestroy()`_. Это можно сделать следующей строкой кода:

```c#
WorldObject.RemoveComponent(this)
```

Тем же методом можно удалить и любой другой компонент этого объекта
извне.
Чтобы удалить другой компонент, вызвав `OnDestroy()`, используйте

```c#
WorldObject.DestroyComponent<T>() //где T - соответствующий тип
```

Для добавления нового компонента используйте:

```c#
WorldObject.AddComponent(component)
```

При этом не следует использовать этот метод в конструкторе 
класса-наследника _`WorldObject`_, так как он вызывает _`Start()`_ компонента.
О том, чем это опасно - в следующем разделе.

#### Жизненный цикл и события компонента
Инициализация компонента начинается с конструктора. Здесь следует
задавать все параметры, которые не могут быть получены из других
компонентов. В конструктор также передаётся тот объект, которому будет
принадлежать данный компонент.

Далее вызывается метод _`Start()`_. Здесь следует через _`GetComponent`_
и _`GetComponents`_ получить все компоненты, которые определены для объекта
изначально и будут использованы в будущем. Вызов указанных методов
довольно трудоёмок, поэтому следует минимизировать количество их вызовов. 
Для этого, как правило, и используется _`Start`_.

По этой причине _`Start`_ должен вызываться уже после того, как в объект
добавлены все основные компоненты. Поэтому в конструкторе объекта
компоненты добавляются через

```c#
components.Add(component)
```

А в дальнейшем через

```c#
AddComponent(component)
```

Далее, пока компонент или объект не будет уничтожен, вызывается метод
_`Update()`_. Этот метод вызывается каждый тик мирового таймера и здесь,
соответственно, прописывается всё то, что должно происходить какждый
тик. Если какой-то компонент в своём _`Update`-методе_ уничтожил объект, 
то для следующих компонент в списке _`Update()`_ вызван не будет.

Перед уничтожением компонента вызывается метод _`OnDestroy()`_. При 
уничтожении объекта сначала вызывается метод _`OnDestroy()`_, а потом уже
выполняется отвязка компонентов от него, поэтому в методе _`OnDestroy()`_
можно обращаться к любым компонентам объекта.

Чаще всего _`OnDestroy()`_ используется для создания на месте старого
объекта нового. Например, при уничтожении _`EggComponent`_ создаётся
новый _`Animal`_.

#### Обобщения

#### Список существующих компонентов и их назначения
```c#
InformationComponent
```
Имеет два режима: активный и пассивный.

При переходе в активный режим получает все компоненты 
объекта, реализующие интерфейс `IHaveInformation`,
сортирует их по значению, возвращаемому 
`GetInformationPriority()`.  

А активном режиме каждый тик формирует новую строку
состояния объекта, обращаясь к методу `ToString()` 
компонент из своего списка.

При переходе в пассивный режим очищает список и строку.

Методы:
```c#
void Open() //переводит компонент в активный режим
void Close() //переводит компонент в пассивный режим
void AddComponent(IHaveInformation component)
            //добавляет указанный компонент в список,
            //если InformationComponent активен
```

___
```c#
DependingOnWeatherComponent
```
При старте получает список всех компонент объекта,
реализующих интерфейс `IDependingOnWeather`.

Каждый тик вызывает `ConfigureByWeather()` у всех
компонентов из списка.

Методы:
```c#
void AddComponent(IDependingOnWeather component)
            //добавляет указанный компонент в список
```

___
```c#
HealthComponent
```


___
### Мировые объекты
#### Конструктор и фабричные методы
#### Список существующих объектов