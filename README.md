<p align="center">
  <a>
    <img src="UP logo.png" alt="Logo" height="100">
  </a>

<h2 align="center">Fakulteti i Inxhinierisë Elektrike dhe Kompjuterike</h2>
<h3 align="center">Lënda: Rrjeta Kompjuterike</h3>
<h2 align="center">Projekti: Sistem për komunikim me sockets, me protokollin TCP i ndërtuar me arkuitekturën klient - server</h2>
<p align="left">Profesori: Blerim Rexha</p>
<p align="left">Asistenti: Mërgim Hoti</p>

<p align="left">Studentët: Edon Gashi, Elona Fetahu, Dreni Nimanaj, Eden Dobra</p><br><br>

</p><br>

## Përmbledhje e Projektit

Ky sistem mundëson ndërveprim të klientëve me server, duke u japur atyre mundësi për kërkesa të ndryshme. Komunikimi bëhet me sockets përmes protokollit TCP. Lidhja e klientit dhe serverit bëhet duke ditur IP dhe portin në të cilin serveri dëgjon. Serveri mund të menaxhojë disa klientë dhe ti përpunojë kërkesat e të gjithë klientëve që janë të lidhur. Sistemi është i zhvilluar në gjuhën programuese C#.

<br><br>

## Funksionet
Meqë ky sistem mund të përdoret në pajisje të ndryshme, duke ndarë rolin e serverit dhe klientit atëherë po i listojmë funksionet e tij duke i ndarë në këto dy role:

- Serveri
  - Mundëson dëgjimin e kërkesave në një port të caktuar
  - Shfaq klientët e lidhur në formë tabelare me të dhënat e tyre
  - Mundëson menaxhimin e përmissions për secilin klient në mënyrë të veçantë
  - Shfaq kërkesat dhe rrjedhjen e tyre
  - Shfaq mesazhet e klientëve
 
- Klienti
  - Mundëson lidhjen me një server përmes IP dhe portit në të cilin ai dëgjon
  - Mundëson dërgimin e mesazheve tek serveri
  - Mundëson Leximin e një file specifik nga serveri, duke e shkarkuar atë në lokacionin (path) të dëshiruar
  - Mundëson Shkrimin e një file specifik në server
  - Mundëson shfaqjen e rezultatit pas ekzekutimit në server të një kodi të shkruar në Java

 <br><br>
## Përdorimi
Ky sistem ka një përdorim mjaftë të thjeshtë për shkak të një dizajni të mirë të GUI. Ka dy ndërfaqe kryesore që është ajo për klient dhe për server.Në vazhdim përmes disa GIF ilustrimeve kemi treguar mënyrën se si përdoret ky program

- Aktivizimi i serverit për dëgjim në një port të caktuar dhe lidhja e klientit me server.
   - Në momentin që butoni Listen klikohet, ai do ta ndërrojë ngjyrën për të sinjalizuar se dëgjimi është duke u bërë në portin e specifikuar.
   - Nëse klienti tenton që të kyçet në një port me numër tjetër, lidhja dështon dhe i shfaqet një mesazh sinjalizues brenda një dritare të re.
<p align="center">
<a>
    <img src="GIF 1 - Listening and connecting.gif" alt="Logo" height="430" align="center">
  </a>
  </p>
<br><br><br>

- Caktimi i privilegjeve nga serveri dhe dërgimi tek klienti i emrave të fileve që gjenden në një lokacion (path) të specifikuar.
   - Serveri e zgjedh klientin që dëshiron të ja modifikojë qasjen dhe pas zgjedhjeve të nivelit të qasjes. Ka mundësinë që ti modifikojë ato qasje përmes butonit "Update permission". Llojet e qasjeve që serveri e ka mundësinë që ti modifikojë janë:
      - Read
      - Write
      - Execute
  - Përmes butonit "Update file access" serveri i dërgon emrat e fileve që gjenden brenda lokacionit të caktuar në fushën "Shared folder path"
  - Tek klienti emrat e fileve shfaqen në fushën "Server files"
<p align="center">
<a>
    <img src="GIF 2 - Giving permissions and sharing files.gif" alt="Logo" height="350" align="center">
  </a>
</p>

<br><br><br>

- Dërgimi i mesazheve nga klienti dhe shfaqja e tyre tek serveri
  - Klienti e shkruan mesazhin e tij në fushën "Data" dhe e klikon butonin "Send"
  - Tek serveri në menunë "Chat" shfaqet emri i klientit që ka dërguar mesazh, si dhe mesazhi që e ka dërguar
<p align="center">
<a>
    <img src="GIF 3 - exchanging messages.gif" alt="Logo" height="350" align="center">
  </a>
</p>

<br><br><br>

- Leximi dhe shkrimi nga klienti i fileve të cilat serveri i ka japur privilegjin
  - Tek klienti klikohet njëri nga filet dhe pastaj secili buton do ta kryej funksionin e caktuar
    - Nëse tentojmë të bëjmë ndonjë funksion për të cilin nuk kemi permission atëherë do të sinjalizohemi për këtë sepse serveri kthen mesazhin "SERVER WARNING:..."
    - Për read e caktojmë lokacionin (path) në fushën "File saving path", në të cilin dëshirojmë të na shkarkohet file që ne e zgjedhim
    - Për write e shkruajmë mesazhin në fushën "Data", e zgjedhim filen ku dëshirojmë të shkruajmë dhe klikojmë butonin "Write"
    - Shkrimi lejohet vetëm në filet e formatit ".txt"
    - Për execute e zgjedhim filen që dëshirojmë ta ekzekutojmë, klikojmë butonin "Execute" dhe rezultati i ekzekutimit do të shfaqet në fushën "Output" si "EXECUTION RESULT:..."
<p align="center">
<a>
    <img src="GIF 4 - Reading and Writing.gif" alt="Logo" height="430" align="center">
  </a>
</p>

   <a href="#top">Kthehu në fillim ↑</a>
