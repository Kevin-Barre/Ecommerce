from pydantic import BaseModel
from typing import Optional



class VentaDetalles (BaseModel): 
     Id: Optional[int]
     VentaId: int
     NumeroItem: int
     ProductoId: int 
     PrecioUnitario: float
     Cantidad: float
     Total: float
     