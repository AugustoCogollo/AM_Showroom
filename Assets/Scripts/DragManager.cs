using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private const int BOTON_IZQUIERDO_MOUSE = 0;
    private Camera camara;
    private GameObject objectoArrastrable;
    private Vector3 screenSpace;
    private bool hayObjetoSeleccionado = false;
    private string nombreDelObjetoSeleccionado = "";
    RaycastHit hitInfo;
    Ray ray;

    // Use this for initialization
    void Start()
    {
        camara = Camera.main;
    }

    void Update()
    {
        if (estaPresionadoElBotonIzquieroDelMouse)
            IntentaSeleccionarUnObjeto();
        else if (estaLevantadoElBotonIzquierdoDelMouse)
            IntentaLiberarObjectoSeleccionado();
    }

    private bool estaPresionadoElBotonIzquieroDelMouse { get => Input.GetMouseButton(BOTON_IZQUIERDO_MOUSE); }

    private void IntentaSeleccionarUnObjeto()
    {
        DisparaRayoDeCamara();
        ConElRayoDeLaCamaraIntentaSeleccionarUnObjeto();
        SiExisteObjetoSeleccionadoArrastralo();
    }

    #region FUNCIONES DE IntentaSeleccionUnObjeto()

    private void DisparaRayoDeCamara()
    {
        ray = camara.ScreenPointToRay(Input.mousePosition);
    }

    private void ConElRayoDeLaCamaraIntentaSeleccionarUnObjeto()
    {
        if (!hayObjetoSeleccionado)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                objectoArrastrable = hitInfo.collider.gameObject;
                nombreDelObjetoSeleccionado = objectoArrastrable.name;
                print(nombreDelObjetoSeleccionado);
                screenSpace = camara.WorldToScreenPoint(objectoArrastrable.transform.position);
                hayObjetoSeleccionado = true;
            }
        }

    }

    private void SiExisteObjetoSeleccionadoArrastralo()
    {
        if (hayObjetoSeleccionado)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            Vector3 currentPosition = camara.ScreenToWorldPoint(currentScreenSpace);
            objectoArrastrable.transform.position = currentPosition;
        }
    }
    #endregion

    private bool estaLevantadoElBotonIzquierdoDelMouse { get => Input.GetMouseButtonUp(BOTON_IZQUIERDO_MOUSE); }
    private void IntentaLiberarObjectoSeleccionado()
    {
        if (hayObjetoSeleccionado)
            LiberaElObjectoSeleccionado();
    }

    #region FUNCIONES_DE IntentaLiberarObejoSeleccionado
    private void LiberaElObjectoSeleccionado()
    {
        hayObjetoSeleccionado = false;
        objectoArrastrable = null;
    }
    #endregion




}