from itertools import combinations

def suma_subconjunto_cercana(array, objetivo):
    mejor_suma = float('inf')
    subconjuntos_mejorados = []

    # Generar todos los subconjuntos posibles
    for r in range(len(array) + 1):  # r es el tamaño del subconjunto
        for subset in combinations(array, r):
            suma_subset = sum(subset)
            diferencia = abs(objetivo - suma_subset)

            # Verificar si la suma es más cercana al objetivo
            if diferencia < abs(objetivo - mejor_suma):
                mejor_suma = suma_subset
                subconjuntos_mejorados = [subset]
            elif diferencia == abs(objetivo - mejor_suma):
                subconjuntos_mejorados.append(subset)

    return subconjuntos_mejorados

# Ejemplos de uso
array1 = [1, 2, 3, 4, 5, 8, 6, 0, 7]
objetivo1 = 10
result1 = suma_subconjunto_cercana(array1, objetivo1)
print("Subconjuntos más cercanos a", objetivo1, ":", result1)

array2 = [2, 2, 8, 10, 1, 8, 3, 5, 9]
objetivo2 = 10
result2 = suma_subconjunto_cercana(array2, objetivo2)
print("Subconjuntos más cercanos a", objetivo2, ":", result2)
