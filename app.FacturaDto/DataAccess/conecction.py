import psycopg2

def getConnection():
    return psycopg2.connect(
        host="database-postgres",
        port="5432",
        dbname="BDDFACTURA",
        user="postgres",
        password="admin"
    )
