# PW_LAB_2
Programowanie Współbieżne Laboratorium 2

Należy zbudować aplikację wielowątkową, gdzie dwie grupy wątków programu w losowych
odstępach czasu korzystają z określonego zasobu. Zasobem może być dowolny obiekt w
pamięci operacyjnej, który może zawierać tablicę wartości zliczanych czasów jaki zajmują
zasób  dane   wątki  i  zliczone  czasy pomiędzy tym   kiedy  wątek  wyraził   chęć  dostępu   do
zasobu, a ją otrzymał. Pierwsza grupa wątków używa zasobu dziesięciokrotnie częściej i dłużej niż druga grupa
wątków.   Można   to   osiągnąć   poprzez   losowe   określanie   czasu   blokowania   zasobu   i
ponownej chęci dostępu do zasobu. Należy   rozwiązać   problem   sprawiedliwego   dostępu   przez   wszystkie   wątki   za   pomocą
mechanizmu programowania współbieżnego zwanego monitorem. W   zależności   od   środowiska   programistycznego   użytego   w   zadaniu   należy   zbudować
odpowiedni obiekt, który będzie pełnił funkcję monitora. Zasady  sprawiedliwego   dostępu   do   zasobu   należy   określić   samodzielnie   opisując   je   w
sprawozdaniu. Wybór należy uzasadnić wspierając się pomierzonymi czasami.
Należy   zwrócić   uwagę   na   problem   zagłodzenia   wątków   i   ich   wzajemne   zakleszczenie,
które nie powinno występować.
Monitor
Monitor względem programisty nie jest procesem, lecz statycznym modułem zawierającym
deklaracje zmiennych i procedur. 
Realizacją   wzajemnego   wykluczania   w   dostępie   do   monitorów   zarządza   środowisko,   w
ramach  którego   funkcjonuje   monitor.   Wątek,   który  wywołuje   publiczną   metodę  monitora
jest:
• wpuszczany do monitora, jeśli w monitorze nie ma innych wątków
• ustawiany w kolejce oczekujących wątków, jeśli w monitorze znajduje się wątek.
Po   opuszczeniu   monitora   przez   wątek   system   wznawia   działanie   pierwszego   wątku   w kolejce.
Istnieje jedna kolejka dla monitora (obsługuje próby wywołań wszystkich funkcji monitora).

