import re
from collections import Counter

def contar_frecuencia_palabras(input_file, output_file):
    try:
        # Inicializar un contador para las palabras
        contador = Counter()

        # Abrir el archivo en modo lectura
        with open(input_file, 'r', encoding='utf-8') as file:
            for linea in file:
                # Convertir a minúsculas
                linea = linea.lower()
                # Eliminar signos de puntuación y caracteres especiales
                linea = re.sub(r'[^a-z\s]', '', linea)
                # Dividir la línea en palabras
                palabras = linea.split()
                # Actualizar el contador con las palabras de la línea
                contador.update(palabras)

        # Abrir el archivo de salida en modo escritura
        with open(output_file, 'w', encoding='utf-8') as out_file:
            for palabra, frecuencia in contador.items():
                # Escribir cada palabra y su frecuencia en el archivo de salida
                out_file.write(f'{palabra}: {frecuencia}\n')

        print(f'El conteo de palabras se ha guardado en {output_file}.')

    except FileNotFoundError:
        print(f'Error: El archivo {input_file} no fue encontrado.')
    except Exception as e:
        print(f'Ocurrió un error: {e}')

# Ejemplo de uso
input_file = './texto_grande.txt'  # Reemplaza con la ruta de tu archivo
output_file = 'resultado_frecuencia.txt'  # Nombre del archivo de salida
contar_frecuencia_palabras(input_file, output_file)
