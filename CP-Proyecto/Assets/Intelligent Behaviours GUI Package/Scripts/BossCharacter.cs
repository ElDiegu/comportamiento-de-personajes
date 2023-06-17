using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCharacter : MonoBehaviour {

    #region variables

    private BehaviourTreeEngine NewBT_BT;
    

    private SelectorNode Root;
    private SequenceNode Campear;
    private SequenceNode Powerup;
    private SequenceNode Atacar;
    private LeafNode Haymonedacerca;
    private LeafNode Moverseamoneda;
    private SelectorNode Esperar;
    private LeafNode Elcontadorhafinalizado;
    private SequenceNode NewSequenceNode3;
    private LeafNode Contadoraumenta;
    private LeafNode Hayalguienenrango;
    private LeafNode Atacar2;
    private LeafNode Estáelpowerupenrangodevision;
    private LeafNode Moversealpowerup;
    private LeafNode Cogerpowerup;
    private SequenceNode Aumentodevelocidad;
    private LeafNode Tiene5powerups;
    private LeafNode Aumentalavelocidad;
    private LeafNode Hayalguienenrangodeataque;
    private SequenceNode Atacando;
    private LeafNode Estádentrodelcampodevisión;
    private LeafNode Moversehaciaél;
    private SequenceNode Comprobacióndelataque;
    private LeafNode Endistanciaparaatacarlo;
    private LeafNode Atacarlo;
    private LeafNode Moverse;
    private InverterDecoratorNode Inverter_Esperar;
    private LoopUntilFailDecoratorNode LoopUntilFail_Inverter_Esperar;
    private SucceederDecoratorNode Succeeder_Aumentodevelocidad;
    private SucceederDecoratorNode Succeeder_Comprobacióndelataque;
    private LoopUntilFailDecoratorNode LoopUntilFail_Atacando;
    
    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        NewBT_BT = new BehaviourTreeEngine(false);
        

        CreateBehaviourTree();
    }
    
    
    private void CreateBehaviourTree()
    {
        // Nodes
        Root = NewBT_BT.CreateSelectorNode("Root");
        Campear = NewBT_BT.CreateSequenceNode("Campear", false);
        Powerup = NewBT_BT.CreateSequenceNode("Power up", false);
        Atacar = NewBT_BT.CreateSequenceNode("Atacar", false);
        Haymonedacerca = NewBT_BT.CreateLeafNode("¿Hay moneda cerca?", HaymonedacercaAction, HaymonedacercaSuccessCheck);
        Moverseamoneda = NewBT_BT.CreateLeafNode("Moverse a moneda", MoverseamonedaAction, MoverseamonedaSuccessCheck);
        Esperar = NewBT_BT.CreateSelectorNode("Esperar");
        Elcontadorhafinalizado = NewBT_BT.CreateLeafNode("¿El contador ha finalizado?", ElcontadorhafinalizadoAction, ElcontadorhafinalizadoSuccessCheck);
        NewSequenceNode3 = NewBT_BT.CreateSequenceNode("New Sequence Node 3", false);
        Contadoraumenta = NewBT_BT.CreateLeafNode("Contador aumenta", ContadoraumentaAction, ContadoraumentaSuccessCheck);
        Hayalguienenrango = NewBT_BT.CreateLeafNode("¿Hay alguien en rango?", HayalguienenrangoAction, HayalguienenrangoSuccessCheck);
        Atacar2 = NewBT_BT.CreateLeafNode("Atacar 2", Atacar2Action, Atacar2SuccessCheck);
        Estáelpowerupenrangodevision = NewBT_BT.CreateLeafNode("¿Está el power up en rango de vision?", EstáelpowerupenrangodevisionAction, EstáelpowerupenrangodevisionSuccessCheck);
        Moversealpowerup = NewBT_BT.CreateLeafNode("Moverse al power up", MoversealpowerupAction, MoversealpowerupSuccessCheck);
        Cogerpowerup = NewBT_BT.CreateLeafNode("Coger power up", CogerpowerupAction, CogerpowerupSuccessCheck);
        Aumentodevelocidad = NewBT_BT.CreateSequenceNode("Aumento de velocidad", false);
        Tiene5powerups = NewBT_BT.CreateLeafNode("¿Tiene 5 power ups?", Tiene5powerupsAction, Tiene5powerupsSuccessCheck);
        Aumentalavelocidad = NewBT_BT.CreateLeafNode("Aumenta la velocidad", AumentalavelocidadAction, AumentalavelocidadSuccessCheck);
        Hayalguienenrangodeataque = NewBT_BT.CreateLeafNode("¿Hay alguien en rango de ataque?", HayalguienenrangodeataqueAction, HayalguienenrangodeataqueSuccessCheck);
        Atacando = NewBT_BT.CreateSequenceNode("Atacando", false);
        Estádentrodelcampodevisión = NewBT_BT.CreateLeafNode("¿Está dentro del campo de visión?", EstádentrodelcampodevisiónAction, EstádentrodelcampodevisiónSuccessCheck);
        Moversehaciaél = NewBT_BT.CreateLeafNode("Moverse hacia él", MoversehaciaélAction, MoversehaciaélSuccessCheck);
        Comprobacióndelataque = NewBT_BT.CreateSequenceNode("Comprobación del ataque", false);
        Endistanciaparaatacarlo = NewBT_BT.CreateLeafNode("¿En distancia para atacarlo?", EndistanciaparaatacarloAction, EndistanciaparaatacarloSuccessCheck);
        Atacarlo = NewBT_BT.CreateLeafNode("Atacarlo", AtacarloAction, AtacarloSuccessCheck);
        Moverse = NewBT_BT.CreateLeafNode("Moverse", MoverseAction, MoverseSuccessCheck);
        Inverter_Esperar = NewBT_BT.CreateInverterNode("Inverter_Esperar", Esperar);
        LoopUntilFail_Inverter_Esperar = NewBT_BT.CreateLoopUntilFailNode("LoopUntilFail_Inverter_Esperar", Inverter_Esperar);
        Succeeder_Aumentodevelocidad = NewBT_BT.CreateSucceederNode("Succeeder_Aumentodevelocidad", Aumentodevelocidad);
        Succeeder_Comprobacióndelataque = NewBT_BT.CreateSucceederNode("Succeeder_Comprobacióndelataque", Comprobacióndelataque);
        LoopUntilFail_Atacando = NewBT_BT.CreateLoopUntilFailNode("LoopUntilFail_Atacando", Atacando);
        
        // Child adding
        Root.AddChild(Campear);
        Root.AddChild(Powerup);
        Root.AddChild(Atacar);
        Root.AddChild(Moverse);
        
        Campear.AddChild(Haymonedacerca);
        Campear.AddChild(Moverseamoneda);
        Campear.AddChild(LoopUntilFail_Inverter_Esperar);
        
        Powerup.AddChild(Estáelpowerupenrangodevision);
        Powerup.AddChild(Moversealpowerup);
        Powerup.AddChild(Cogerpowerup);
        Powerup.AddChild(Succeeder_Aumentodevelocidad);
        
        Atacar.AddChild(Hayalguienenrangodeataque);
        Atacar.AddChild(LoopUntilFail_Atacando);
        
        Esperar.AddChild(Elcontadorhafinalizado);
        Esperar.AddChild(NewSequenceNode3);
        Esperar.AddChild(Contadoraumenta);
        
        NewSequenceNode3.AddChild(Hayalguienenrango);
        NewSequenceNode3.AddChild(Atacar2);
        
        Aumentodevelocidad.AddChild(Tiene5powerups);
        Aumentodevelocidad.AddChild(Aumentalavelocidad);
        
        Atacando.AddChild(Estádentrodelcampodevisión);
        Atacando.AddChild(Moversehaciaél);
        Atacando.AddChild(Succeeder_Comprobacióndelataque);
        
        Comprobacióndelataque.AddChild(Endistanciaparaatacarlo);
        Comprobacióndelataque.AddChild(Atacarlo);
        
        // SetRoot
        NewBT_BT.SetRootNode(Root);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        NewBT_BT.Update();
    }

    // Create your desired actions
    
    private void HaymonedacercaAction()
    {
        
    }
    
    private ReturnValues HaymonedacercaSuccessCheck()
    {
        //Write here the code for the success check for Haymonedacerca
        return ReturnValues.Failed;
    }
    
    private void MoverseamonedaAction()
    {
        
    }
    
    private ReturnValues MoverseamonedaSuccessCheck()
    {
        //Write here the code for the success check for Moverseamoneda
        return ReturnValues.Failed;
    }
    
    private void ElcontadorhafinalizadoAction()
    {
        
    }
    
    private ReturnValues ElcontadorhafinalizadoSuccessCheck()
    {
        //Write here the code for the success check for Elcontadorhafinalizado
        return ReturnValues.Failed;
    }
    
    private void ContadoraumentaAction()
    {
        
    }
    
    private ReturnValues ContadoraumentaSuccessCheck()
    {
        //Write here the code for the success check for Contadoraumenta
        return ReturnValues.Failed;
    }
    
    private void HayalguienenrangoAction()
    {
        
    }
    
    private ReturnValues HayalguienenrangoSuccessCheck()
    {
        //Write here the code for the success check for Hayalguienenrango
        return ReturnValues.Failed;
    }
    
    private void Atacar2Action()
    {
        
    }
    
    private ReturnValues Atacar2SuccessCheck()
    {
        //Write here the code for the success check for Atacar2
        return ReturnValues.Failed;
    }
    
    private void EstáelpowerupenrangodevisionAction()
    {
        
    }
    
    private ReturnValues EstáelpowerupenrangodevisionSuccessCheck()
    {
        //Write here the code for the success check for Estáelpowerupenrangodevision
        return ReturnValues.Failed;
    }
    
    private void MoversealpowerupAction()
    {
        
    }
    
    private ReturnValues MoversealpowerupSuccessCheck()
    {
        //Write here the code for the success check for Moversealpowerup
        return ReturnValues.Failed;
    }
    
    private void CogerpowerupAction()
    {
        
    }
    
    private ReturnValues CogerpowerupSuccessCheck()
    {
        //Write here the code for the success check for Cogerpowerup
        return ReturnValues.Failed;
    }
    
    private void Tiene5powerupsAction()
    {
        
    }
    
    private ReturnValues Tiene5powerupsSuccessCheck()
    {
        //Write here the code for the success check for Tiene5powerups
        return ReturnValues.Failed;
    }
    
    private void AumentalavelocidadAction()
    {
        
    }
    
    private ReturnValues AumentalavelocidadSuccessCheck()
    {
        //Write here the code for the success check for Aumentalavelocidad
        return ReturnValues.Failed;
    }
    
    private void HayalguienenrangodeataqueAction()
    {
        
    }
    
    private ReturnValues HayalguienenrangodeataqueSuccessCheck()
    {
        //Write here the code for the success check for Hayalguienenrangodeataque
        return ReturnValues.Failed;
    }
    
    private void EstádentrodelcampodevisiónAction()
    {
        
    }
    
    private ReturnValues EstádentrodelcampodevisiónSuccessCheck()
    {
        //Write here the code for the success check for Estádentrodelcampodevisión
        return ReturnValues.Failed;
    }
    
    private void MoversehaciaélAction()
    {
        
    }
    
    private ReturnValues MoversehaciaélSuccessCheck()
    {
        //Write here the code for the success check for Moversehaciaél
        return ReturnValues.Failed;
    }
    
    private void EndistanciaparaatacarloAction()
    {
        
    }
    
    private ReturnValues EndistanciaparaatacarloSuccessCheck()
    {
        //Write here the code for the success check for Endistanciaparaatacarlo
        return ReturnValues.Failed;
    }
    
    private void AtacarloAction()
    {
        
    }
    
    private ReturnValues AtacarloSuccessCheck()
    {
        //Write here the code for the success check for Atacarlo
        return ReturnValues.Failed;
    }
    
    private void MoverseAction()
    {
        
    }
    
    private ReturnValues MoverseSuccessCheck()
    {
        //Write here the code for the success check for Moverse
        return ReturnValues.Failed;
    }
    
}