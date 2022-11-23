
using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{

    class Estrategia
	{
		//Consulta - 1
		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			String texto = "";
			if(arbol.esHoja()){
				DecisionData result = arbol.getDatoRaiz();
				texto = texto + "·" + result.ToString() + "\n";
				return texto;
			}	
			if(arbol.getHijoIzquierdo() != null){
				texto = texto + this.Consulta1(arbol.getHijoIzquierdo());
			}
			
			if(arbol.getHijoDerecho() != null){
				texto = texto + this.Consulta1(arbol.getHijoDerecho());
			}
			return texto;
		}
		
		//Consulta - 2
		public String Consulta2(ArbolBinario<DecisionData> arbol)
		{
			String texto = "--- CAMINOS HACIA LOS PERSONAJES --- \n\n";
			List<ArbolBinario<DecisionData>> nodos = new List<ArbolBinario<DecisionData>>();
			List<ArbolBinario<DecisionData>> caminos = new List<ArbolBinario<DecisionData>>();
			caminos = recorrido(nodos, caminos, arbol);
			foreach(var elem in caminos){
				texto = texto + elem.getDatoRaiz().ToString() + " | ";
				if(elem.esHoja()){
					texto = texto + "\n\n";
				}
			}

			return texto;
		}
		
		//Metodo Auxiliar
		private List<ArbolBinario<DecisionData>> recorrido(List<ArbolBinario<DecisionData>> nodos, List<ArbolBinario<DecisionData>> caminos, ArbolBinario<DecisionData> arbol){
			nodos.Add(arbol);
			if(arbol.esHoja()){
				caminos.AddRange(nodos);
			}
			if(arbol.getHijoIzquierdo() != null){
				recorrido(nodos, caminos, arbol.getHijoIzquierdo());
				nodos.RemoveAt(nodos.Count - 1);
			}
			if(arbol.getHijoDerecho() != null){
				recorrido(nodos, caminos, arbol.getHijoDerecho());
				nodos.RemoveAt(nodos.Count - 1);
			}
			return caminos;
		}
		
		//Consulta - 3
		public String Consulta3(ArbolBinario<DecisionData> arbol)
		{
			Cola<ArbolBinario<DecisionData>> c = new Cola<ArbolBinario<DecisionData>>();
			ArbolBinario<DecisionData> aux;
			String texto = "--- NIVELES DEL ARBOL--- \n\n Nivel 0: \n";
			int nivel = 1;
			
			c.encolar(arbol);
			c.encolar(null);
			
			while(!c.esVacia()){
				aux = c.desencolar();
				if(aux == null){
					if(!c.esVacia()){
						texto = texto + "Nivel " + nivel + ": \n";
						nivel++;
						c.encolar(null);
					}
				}
				else{
					DecisionData d = aux.getDatoRaiz();
					texto = texto + "\t "+ d.ToString() + " \n ";
					
					if(aux.getHijoIzquierdo() != null){
						c.encolar(aux.getHijoIzquierdo());
					}
					
					if(aux.getHijoDerecho() != null){
						c.encolar(aux.getHijoDerecho());
					}
				}
			}
			return texto;
		}

		//Creacion Arból
		public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
		{
			if(clasificador.crearHoja()){
				DecisionData d = new DecisionData(clasificador.obtenerDatoHoja());
				ArbolBinario<DecisionData> nodo = new ArbolBinario<DecisionData>(d);
				return nodo;				
			}
			else{
				DecisionData p = new DecisionData(clasificador.obtenerPregunta());
				ArbolBinario<DecisionData> arbolWiW = new ArbolBinario<DecisionData>(p);
				arbolWiW.agregarHijoIzquierdo(CrearArbol(clasificador.obtenerClasificadorIzquierdo()));
				arbolWiW.agregarHijoDerecho(CrearArbol(clasificador.obtenerClasificadorDerecho()));
				return arbolWiW;
			}
		}
	}
}