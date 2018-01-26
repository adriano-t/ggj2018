Fai il passo 1 e 2


## 1) Apri sempre la console dalla cartella del progetto
* Entra nella cartella cartella dove vuoi che venga creato il progetto
* `Tasto destro del mouse` > `Git Bash Here`
* Oppure se sei un linuxaro, `cd <nome cartella>`

## 2) Configurazione di Git per Windows##
* `git config --global user.name "TUO_USERNAME"`
* `git config --global user.email TUA_MAIL@example.com`


## 3) Clona il progetto da bitbucket.com ##
* `git clone https://TUO_USERNAME@bitbucket.org/Tizzio/mirror.git <opzionale cartella>`
* Inserisci la tua password di bitbucket quando te la chiede
* Fatto, ora hai il progetto.

# Per Ricevere#
## Aggiornare per ricevere i commit degli altri ##
* `git pull`
* se ci sono conflitti puoi resettare i tuoi files scartando i cambiamenti che hai fatto, con 
`git checkout` oppure `git stash`

# Per Salvare e fare il merge#
## Creare un commit (locale) ##
* `git add --all`
* `git commit -m "messaggio"`

## Inviare un commit online ##
* ricordati di fare il `commit` prima
* `git push`

## Altro ##
* `git help <comando>` :  per ottenere un aiuto su un comando (es. $ git help config)
* `git config --global push.default simple`

## Credenziali ##

* Windows

`git config --global credential.helper wincred`

* OSX

`git config --global credential.helper osxkeychain`

* Linux

`git config --global credential.helper cache`

`git config --global credential.helper 'cache --timeout=3600'`