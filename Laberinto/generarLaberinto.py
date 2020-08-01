from random import randint

WIDTH  = 200
HEIGHT = 40
laberinto = [['  ' for j in range(WIDTH)] for i in range(HEIGHT) ]

def conectarSalas( sala_a, sala_b ):
    if sala_b['center'][1] + 1 < sala_a['pos'][1]:
        y1 = sala_b['center'][1]
        y2 = sala_a['pos'][1]
        x1 = sala_a['center'][0]
        x2 = sala_b['pos'][0]
        for i in range( y1-1, y2):
            laberinto[i][x1-1] = '* '
            laberinto[i][x1] = '* '
        for j in range( x1, x2):
            laberinto[y1-1][j] = '* '
            laberinto[y1][j] = '* '
    elif sala_b['center'][1] - 1 > sala_a['pos'][1] + sala_a['tam'][1]:
        y1 = sala_a['pos'][1] + sala_a['tam'][1]
        y2 = sala_b['center'][1]
        x1 = sala_a['center'][0]
        x2 = sala_b['pos'][0]
        for i in range( y1, y2):
            laberinto[i][x1-1] = '* '
            laberinto[i][x1] = '* '
        for j in range( x1-1, x2):
            laberinto[y2-1][j] = '* '
            laberinto[y2][j] = '* '
    else:
        yy = ( max( sala_a['pos'][1], sala_b['pos'][1] ) + min( sala_a['pos'][1] + sala_a['tam'][1], sala_b['pos'][1] + sala_b['tam'][1]) )//2
        x1 = sala_a['pos'][0] + sala_a['tam'][0]
        x2 = sala_b['pos'][0]
        for j in range(x1, x2):
            laberinto[yy-1][j] = "* "
            laberinto[yy][j] = "* "

# Creamos cada sala del juego
widthRemaining = WIDTH
salas = []
[x, y] = [0, 0]
while widthRemaining > 0:
    width    = randint(3, 6) * 2
    height   = width # randint(3, 4) * 2
    distance = randint(2, 3) * 2

    y = randint(0, (HEIGHT - height)//2 - 1) * 2

    widthRemaining = widthRemaining - width - distance
    if widthRemaining < 20:
        width += widthRemaining
        widthRemaining = 0
    
    sala = {}
    sala['pos'] = [x, y]
    sala['tam'] = [width, height]
    sala['center'] = [ x + width//2, y + height//2 ]
    salas.append( sala )
    
    [x, y] = [x + width + distance, 0]

# Creamos las conexiones entre las salas
for i in range(len(salas) - 1):
    conectarSalas( salas[i], salas[i+1] )

# Actualizamos los valores de la matriz laberinto
for i in range(len(salas)):
    x = salas[i]['pos'][0]
    y = salas[i]['pos'][1]
    w = salas[i]['tam'][0]
    h = salas[i]['tam'][1]
    for i in range( x, x + w ):
        for j in range( y, y + h ):
            laberinto[j][i] = 'X '

# Lo guardamos en un archivo
f = open("fichero.txt", "w")
for i in range(HEIGHT):
    for j in range(WIDTH):
        f.write(laberinto[i][j])
    f.write("\n")
f.close()