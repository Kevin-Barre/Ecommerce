from fastapi import FastAPI, HTTPException
from typing import List
from Entities.FacturaDto import VentaDetalles
from DataAccess.conecction import getConnection

app = FastAPI()

@app.get("/obtenerFacturaDto", response_model=List[VentaDetalles])
def obtener_facturas():
    try:
        conn = getConnection()
        with conn:
            with conn.cursor() as cursor:
                cursor.execute('SELECT "Id", "VentaId", "NumeroItem", "ProductoId", "PrecioUnitario", "Cantidad", "Total" FROM "VentaDetalles"')
                columns = [desc[0] for desc in cursor.description]
                result = cursor.fetchall()
                lista = []
                for row in result:
                    row_dict = dict(zip(columns, row))
                    lista.append(VentaDetalles(**row_dict))
                return lista
    except Exception as e:
        import traceback
        print(traceback.format_exc())
        raise HTTPException(status_code=500, detail=f"Error al obtener la factura: {e}")


@app.get("/FacturaDto/{id}", response_model=VentaDetalles)
def obtener_factura(id= int):
    try:
        conn = getConnection()
        with conn:
            with conn.cursor() as cursor:
                cursor.execute('SELECT "Id", "VentaId", "NumeroItem", "ProductoId", "PrecioUnitario", "Cantidad", "Total" FROM "VentaDetalles" WHERE "Id" = %s', (id,))
                
                row = cursor.fetchone()
                if row is None:
                    return {"message": f"Factura con ID {id} no encontrada", "data": None}
                columns = [desc[0] for desc in cursor.description]
                row_dict = dict(zip(columns, row))
                return VentaDetalles(**row_dict)
    except Exception as e:
        import traceback
        print(traceback.format_exc())
        raise HTTPException(status_code=500, detail=f"Error al obtener la factura: {e}")




if __name__ == "__main__":
    import uvicorn
    import webbrowser
    import threading

    def open_browser():
        webbrowser.open_new("http://127.0.0.1:8000/docs")

    threading.Timer(1.5, open_browser).start()
    uvicorn.run("main:app", host="127.0.0.1", port=8000, reload=True)
 