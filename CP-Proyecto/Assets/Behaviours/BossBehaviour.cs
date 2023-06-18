using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour {

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
    private LeafNode Moversehaciaél;
    private LeafNode Atacarlo;
    private LeafNode Moverse;
    private LeafNode Moverse1;
    private LeafNode DestruirMoneda;
    private InverterDecoratorNode Inverter_Esperar;
    private InverterDecoratorNode Inversor;

    private LoopUntilFailDecoratorNode LoopUntilFail_Inverter_Esperar;
    private SucceederDecoratorNode Succeeder_Aumentodevelocidad;

    //Place your variables here
    [Header("Character Scripts")]
    [SerializeField] BossMovement entityMovement;
    [SerializeField] BossInv entityInv;
    [SerializeField] BossInteraction entityInteraction;
    [SerializeField] BossFieldOfView fow;
    [SerializeField] float waitedTime;

    GameObject powerUpLooked;
    GameObject coinLooked;

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
        Moverse1 = NewBT_BT.CreateLeafNode("Moverse1", Moverse1Action, Moverse1SuccessCheck);
        Atacar2 = NewBT_BT.CreateLeafNode("Atacar 2", Atacar2Action, Atacar2SuccessCheck);
        Estáelpowerupenrangodevision = NewBT_BT.CreateLeafNode("¿Está el power up en rango de vision?", EstáelpowerupenrangodevisionAction, EstáelpowerupenrangodevisionSuccessCheck);
        Moversealpowerup = NewBT_BT.CreateLeafNode("Moverse al power up", MoversealpowerupAction, MoversealpowerupSuccessCheck);
        Cogerpowerup = NewBT_BT.CreateLeafNode("Coger power up", CogerpowerupAction, CogerpowerupSuccessCheck);
        Aumentodevelocidad = NewBT_BT.CreateSequenceNode("Aumento de velocidad", false);
        Tiene5powerups = NewBT_BT.CreateLeafNode("¿Tiene 5 power ups?", Tiene5powerupsAction, Tiene5powerupsSuccessCheck);
        Aumentalavelocidad = NewBT_BT.CreateLeafNode("Aumenta la velocidad", AumentalavelocidadAction, AumentalavelocidadSuccessCheck);
        Hayalguienenrangodeataque = NewBT_BT.CreateLeafNode("¿Hay alguien en rango de ataque?", HayalguienenrangodeataqueAction, HayalguienenrangodeataqueSuccessCheck);;
        Moversehaciaél = NewBT_BT.CreateLeafNode("Moverse hacia él", MoversehaciaélAction, MoversehaciaélSuccessCheck);
        Atacarlo = NewBT_BT.CreateLeafNode("Atacarlo", AtacarloAction, AtacarloSuccessCheck);
        Moverse = NewBT_BT.CreateLeafNode("Moverse", MoverseAction, MoverseSuccessCheck);
        Inverter_Esperar = NewBT_BT.CreateInverterNode("Inverter_Esperar", Esperar);
        LoopUntilFail_Inverter_Esperar = NewBT_BT.CreateLoopUntilFailNode("LoopUntilFail_Inverter_Esperar", Inverter_Esperar);
        Succeeder_Aumentodevelocidad = NewBT_BT.CreateSucceederNode("Succeeder_Aumentodevelocidad", Aumentodevelocidad);
        DestruirMoneda = NewBT_BT.CreateLeafNode("Destruir moneda", DestruirMonedaAction, DestruirMonedaSuccessCheck);
        Inversor = NewBT_BT.CreateInverterNode("Inversor", Contadoraumenta);

        LoopDecoratorNode rootNode = NewBT_BT.CreateLoopNode("Root node", Root);

        // Child adding
        Root.AddChild(Campear);
        Root.AddChild(Powerup);
        Root.AddChild(Atacar);
        Root.AddChild(Moverse);
        
        Campear.AddChild(Haymonedacerca);
        Campear.AddChild(Moverseamoneda);
        Campear.AddChild(LoopUntilFail_Inverter_Esperar);
        Campear.AddChild(DestruirMoneda);
        
        Powerup.AddChild(Estáelpowerupenrangodevision);
        Powerup.AddChild(Moversealpowerup);
        Powerup.AddChild(Cogerpowerup);
        Powerup.AddChild(Succeeder_Aumentodevelocidad);
        
        Atacar.AddChild(Hayalguienenrangodeataque);
        Atacar.AddChild(Moversehaciaél);
        Atacar.AddChild(Atacarlo);

        Esperar.AddChild(Elcontadorhafinalizado);
        Esperar.AddChild(NewSequenceNode3);
        Esperar.AddChild(Inversor);
        
        NewSequenceNode3.AddChild(Hayalguienenrango);
        NewSequenceNode3.AddChild(Moverse1);
        NewSequenceNode3.AddChild(Atacar2);
        
        Aumentodevelocidad.AddChild(Tiene5powerups);
        Aumentodevelocidad.AddChild(Aumentalavelocidad);
        
      
        // SetRoot
        NewBT_BT.SetRootNode(rootNode);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        NewBT_BT.Update();
        Debug.Log(NewBT_BT.GetCurrentState().Name);
    }

    // Create your desired actions
    
    private void HaymonedacercaAction(){ Debug.Log("Moneda"); }
    private ReturnValues HaymonedacercaSuccessCheck()
    {
        if (fow.coin != null)
        {
            coinLooked = fow.coin;
            return ReturnValues.Succeed;
        }else
        { 
            return ReturnValues.Failed;
        }
    }
  
    private void MoverseamonedaAction() { entityMovement.Follow(coinLooked); }
    private ReturnValues MoverseamonedaSuccessCheck()
    {
        if (EntityInv.inRange(gameObject, coinLooked)){
            return ReturnValues.Succeed;
        }
        else {
            return ReturnValues.Running;
        }
    }
    
    private void ElcontadorhafinalizadoAction(){}
    private ReturnValues ElcontadorhafinalizadoSuccessCheck()
    {
        if (waitedTime >= 2.0f) {
            // new Transition("Entry_transition", Elcontadorhafinalizado.StateNode, new PushPerception(NewBT_BT), Moverse.StateNode, NewBT_BT)
            // .FireTransition();
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;   
        }
    }
    
    private void ContadoraumentaAction()
    {
        waitedTime += 10.0f * Time.deltaTime;
        Debug.Log(waitedTime);
    }
    private ReturnValues ContadoraumentaSuccessCheck()
    {
        return ReturnValues.Failed;
    }
    
    private void DestruirMonedaAction(){ entityInv.PickObject(coinLooked); }
    private ReturnValues DestruirMonedaSuccessCheck()
    {
        if (entityInv.isPickingObject)
        {
            return ReturnValues.Running;
        }
        else
        {
            coinLooked = null;
            return ReturnValues.Succeed;
        }
    }
    private void HayalguienenrangoAction(){}
    private ReturnValues HayalguienenrangoSuccessCheck()
    {
        if (fow.enemy != null)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
    private void Moverse1Action() { entityMovement.Follow(fow.enemy); }
    private ReturnValues Moverse1SuccessCheck()
    {
        if (fow.enemy != null) { 
            if (EntityInv.inRange(gameObject, fow.enemy))
            {
                return ReturnValues.Succeed;
            }
            else
            {
                return ReturnValues.Running;
            }
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void Atacar2Action(){ entityInteraction.Attack(fow.enemy); }
    private ReturnValues Atacar2SuccessCheck()
    {
        if (entityInteraction.isAttacking)
        {
            return ReturnValues.Running;
        }
        else
        {
            return ReturnValues.Succeed;
        }
    }
    
    private void EstáelpowerupenrangodevisionAction(){ Debug.Log("Powerup"); }
    private ReturnValues EstáelpowerupenrangodevisionSuccessCheck()
    {
        if (fow.powerUp != null)
        {
            powerUpLooked = fow.powerUp;
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
    
    private void MoversealpowerupAction(){ entityMovement.Follow(powerUpLooked); }
    private ReturnValues MoversealpowerupSuccessCheck()
    {
        if (EntityInv.inRange(gameObject, powerUpLooked)){
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Running;
        }
    }
    
    private void CogerpowerupAction(){ entityInv.PickObject(powerUpLooked); }
    
    private ReturnValues CogerpowerupSuccessCheck()
    {
        if (entityInv.isPickingObject)
        {
            return ReturnValues.Running;
        }
        else
        {
            powerUpLooked = null;
            return ReturnValues.Succeed;
            
        }

    }
    
    private void Tiene5powerupsAction(){}
    
    private ReturnValues Tiene5powerupsSuccessCheck()
    {
        //Write here the code for the success check for Tiene5powerups
        if (entityInv._totalPowerUp >= 5)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
    
    private void AumentalavelocidadAction(){ entityInv.IncreaseVelocity(); }
    
    private ReturnValues AumentalavelocidadSuccessCheck()
    {
        //Write here the code for the success check for Aumentalavelocidad
        if (entityInv.isIncreasingVelocity)
        {
            return ReturnValues.Running;
        }
        else
        {
            return ReturnValues.Succeed;
        }
    }
    
    private void HayalguienenrangodeataqueAction(){}
    
    private ReturnValues HayalguienenrangodeataqueSuccessCheck()
    {
        //Write here the code for the success check for Hayalguienenrangodeataque
        if (fow.enemy != null) {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }

    }
   
    private void MoversehaciaélAction(){ entityMovement.Follow(fow.enemy); }
    private ReturnValues MoversehaciaélSuccessCheck()
    {
        //Write here the code for the success check for Moversehaciaél
        if (fow.enemy != null)
        {
            if(EntityInv.inRange(gameObject, fow.enemy))
            {
                return ReturnValues.Succeed;
            }
            else
            {
                return ReturnValues.Running;
            }
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
    private void AtacarloAction(){ entityInteraction.Attack(fow.enemy); }
    
    private ReturnValues AtacarloSuccessCheck()
    {
        //Write here the code for the success check for Atacarlo
        if (entityInteraction.isAttacking)
        {
            return ReturnValues.Running;
        }
        else
        {
            return ReturnValues.Succeed;
        }
    }
    
    private void MoverseAction()
    {
        entityMovement.MoveRandom();
    }
    
    private ReturnValues MoverseSuccessCheck()
    {
        //Write here the code for the success check for Moverse
        if (entityMovement.isMoving)
        {
            return ReturnValues.Running;
        }
        else
        {
            return ReturnValues.Succeed;
        }
    }
    
    
}