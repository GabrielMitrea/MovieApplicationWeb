# MovieApplication

Deorece urmaresc multe filme/seriale, m-am gandit la o aplicatie in aceasta zona. Aceasta  va fi o aplicatie care stocheaza informatie despre filmele de urmarit/placute/ce urmeaza. Aceasta aplicatie va putea:  stoca informatie despre un anumit film/serial,  va include functie de search si edit/delete/details atunci cand esti logat, totodata avand imagine/poster la fiecare film/serial si un link catre trailer-ul acestora.
Despre filme se mentioneaza : Id-ul, titlul, genul, durata, data lansarii, directorul, descriere, link catre trailer si un poster.
Despre seriale se mentioneaza: Id-ul, titlul, genul,data lansarii, numarul de episoade, numarul de sezoane, directorul, link catre trailer si poster.


## Database Diagram Schema

![diag](https://user-images.githubusercontent.com/61286589/85062853-e9ea5e00-b1b1-11ea-8fa2-fee80c195d0c.png)



In aceasta aplicatie am folosit urmatoarele tabele in baza de date:

* Users
* FavFilms (Filme favorite)
* FavSerials(Seriale favorite)
* Filmss
* Serialss
* ComingSoon
* Genres
* DivertismentTypes

Deoarece genul, tipul de divertisment si user-ul le folosim la mai multe tabele, am creat cate o cheie straina pentru tipul de divertisment, genul tipului de divertisment si user.
