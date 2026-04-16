
using SkifahrenSemaphore;

public static SemaphoreSlim piste   = new SemaphoreSlim(1, 1); // nur 1 Läufer auf der Piste
public static SemaphoreSlim trigger = new SemaphoreSlim(0);    // Läufer weckt Zeitnehmer
public static SemaphoreSlim done    = new SemaphoreSlim(0);    // Zeitnehmer gibt Läufer frei


Racer r1 = new Racer("Racer 1", piste, trigger, done);
Racer r2 = new Racer("Racer 2", piste, trigger, done);
Racer r3 = new Racer("Racer 3", piste, trigger, done);      
Timekeeper tk = new Timekeeper(("Timekeeper", piste, trigger, done));

new Task(() => r1.Run()).Start();
new Task(() => r2.Run()).Start();
new Task(() => r3.Run()).Start();

