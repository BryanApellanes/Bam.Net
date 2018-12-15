# Vyoo

Vyoo is the view engine underneath TVG (Troo, Vyoo, Gloo) which powers the Bamvvm framework.

- Serves all apps that it finds under [ContentRoot]/apps
- Will only serve apps whose ProcessMode matches ProcessMode.Current
- Can set custom response headers using /apps/<appName>/meta/headers/<file_path>
