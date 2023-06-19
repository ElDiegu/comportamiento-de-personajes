using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowardBehaviour : MonoBehaviour {

    #region variables

    private StateMachineEngine cowardCharacter_FSM;
    private StateMachineEngine Inactivo_SubFSM;
    private StateMachineEngine CogerMoneda_SubFSM;
    private StateMachineEngine Huir_SubFSM;
    private StateMachineEngine CogerArmadura_SubFSM;
    
    private ValuePerception EstadoInicialPerception;
    private ValuePerception HayMonedaCerca1Perception;
    private ValuePerception NoHayMonedaCercaNoHayEnemigoCercaPerception;
    private ValuePerception NoHayMonedaCercaHayEnemigoCercaPerception;
    private ValuePerception HayMonedaCerca4Perception;
    private ValuePerception HayEnemigoCercaPerception;
    private ValuePerception HayArmaduraCerca1Perception;
    private ValuePerception NoHayArmaduraCercaPerception;
    private ValuePerception HayMonedaCerca3Perception;
    private ValuePerception NoHayEnemigoCercaPerception;
    private ValuePerception HayMonedaCerca2Perception;
    private State Inactivo;
    private State InactivoEmpty;
    private State CogerMoneda;
    private State Huir;
    private State CogerArmadura;
    private ValuePerception NoEstaMoviendosePerception;
    private ValuePerception NoEstaParadoPerception;
    private State Quieto;
    private State Moverse;
    private ValuePerception EstaEnMonedaPerception;
    private ValuePerception NoEstaCogiendoMonedaPerception;
    private State EnMoneda;
    private State CogiendoMoneda;
    private ValuePerception NoEstaHuyendo1Perception;
    private ValuePerception NoEstaParado1Perception;
    private State Huye;
    private State Descansar;
    private ValuePerception NoEstaCogiendoArmaduraPerception;
    private ValuePerception EstaEnArmaduraPerception;
    private State EnArmadura;
    private State CogiendoArmadura;

    //Place your variables here
    [Header("Character Scripts")]
    [SerializeField] EntityMovement entityMovement;
    [SerializeField] EntityInv entityInv;
    [SerializeField] EntityInteraction entityInteraction;
    [SerializeField] FieldOfView fow;

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        cowardCharacter_FSM = new StateMachineEngine(false);
        Inactivo_SubFSM = new StateMachineEngine(true);
        CogerMoneda_SubFSM = new StateMachineEngine(true);
        Huir_SubFSM = new StateMachineEngine(true);
        CogerArmadura_SubFSM = new StateMachineEngine(true);

        CreateCogerArmadura_SubFSM();
        CreateHuir_SubFSM();
        CreateCogerMoneda_SubFSM();
        CreateInactivo_SubFSM();
        CreateStateMachine();
    }
    
    private void CreateInactivo_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        NoEstaMoviendosePerception = Inactivo_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isMoving);
        NoEstaParadoPerception = Inactivo_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting);
        
        // States
        Quieto = Inactivo_SubFSM.CreateEntryState("Quieto");
        Moverse = Inactivo_SubFSM.CreateState("Moverse");
        
        // Transitions
        Inactivo_SubFSM.CreateTransition("NoEstaHuyendo", Moverse, NoEstaMoviendosePerception, Quieto, Rest);
        Inactivo_SubFSM.CreateTransition("NoEstaParado", Quieto, NoEstaParadoPerception, Moverse, MoveRandom);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateCogerMoneda_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnMonedaPerception = CogerMoneda_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.coin));
        NoEstaCogiendoMonedaPerception = CogerMoneda_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);
        
        // States
        EnMoneda = CogerMoneda_SubFSM.CreateEntryState("En Moneda");
        CogiendoMoneda = CogerMoneda_SubFSM.CreateState("Cogiendo Moneda");
        
        // Transitions
        CogerMoneda_SubFSM.CreateTransition("EstaEnMoneda", EnMoneda, EstaEnMonedaPerception, CogiendoMoneda, PickCoin);
        CogerMoneda_SubFSM.CreateTransition("NoEstaCogiendoMoneda", CogiendoMoneda, NoEstaCogiendoMonedaPerception, EnMoneda);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateHuir_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        NoEstaHuyendo1Perception = Huir_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isFleeing);
        NoEstaParado1Perception = Huir_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting);
        
        // States
        Huye = Huir_SubFSM.CreateEntryState("Huye");
        Descansar = Huir_SubFSM.CreateState("Descansar");
        
        // Transitions
        Huir_SubFSM.CreateTransition("NoEstaHuyendo1", Huye, NoEstaHuyendo1Perception, Descansar, Rest);
        Huir_SubFSM.CreateTransition("NoEstaParado1", Descansar, NoEstaParado1Perception, Huye, Flee);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateCogerArmadura_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        NoEstaCogiendoArmaduraPerception = CogerArmadura_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);
        EstaEnArmaduraPerception = CogerArmadura_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.armor));
        
        // States
        EnArmadura = CogerArmadura_SubFSM.CreateEntryState("En Armadura");
        CogiendoArmadura = CogerArmadura_SubFSM.CreateState("Cogiendo Armadura");
        
        // Transitions
        CogerArmadura_SubFSM.CreateTransition("NoEstaCogiendoArmadura", EnArmadura, EstaEnArmaduraPerception, CogiendoArmadura, PickArmor);
        CogerArmadura_SubFSM.CreateTransition("EstaEnArmadura", CogiendoArmadura, NoEstaCogiendoArmaduraPerception, EnArmadura);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }  
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstadoInicialPerception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => true);
        HayMonedaCerca1Perception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => entityInv.coinDetected);
        NoHayMonedaCercaNoHayEnemigoCercaPerception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => !entityInv.coinDetected && !entityInteraction.enemyDetected);
        NoHayMonedaCercaHayEnemigoCercaPerception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => !entityInv.coinDetected && entityInteraction.enemyDetected);
        HayMonedaCerca4Perception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => entityInv.coinDetected);
        HayEnemigoCercaPerception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => entityInteraction.enemyDetected);
        HayArmaduraCerca1Perception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => entityInv.armorDetected);
        NoHayArmaduraCercaPerception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => !entityInv.armorDetected);
        HayMonedaCerca3Perception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => !entityInv.coinDetected);
        NoHayEnemigoCercaPerception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => !entityInv.armorDetected && !entityMovement.isFleeing);
        HayMonedaCerca2Perception = cowardCharacter_FSM.CreatePerception<ValuePerception>(() => entityInv.coinDetected);
        
        // States
        InactivoEmpty = cowardCharacter_FSM.CreateEntryState("Inactivo Empty", InactivoEmptyAction);
        Inactivo = cowardCharacter_FSM.CreateSubStateMachine("Inactivo", Inactivo_SubFSM);
        CogerMoneda = cowardCharacter_FSM.CreateSubStateMachine("Coger Moneda", CogerMoneda_SubFSM);
        Huir = cowardCharacter_FSM.CreateSubStateMachine("Huir", Huir_SubFSM);
        CogerArmadura = cowardCharacter_FSM.CreateSubStateMachine("Coger Armadura", CogerArmadura_SubFSM);

        // Transitions

        // ExitPerceptions

        // ExitTransitions
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit0", InactivoEmpty, EstadoInicialPerception, Inactivo);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit1", Inactivo, HayMonedaCerca1Perception, CogerMoneda, MoveToCoin);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit2", Inactivo, HayArmaduraCerca1Perception, CogerArmadura, MoveToArmor);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit3", Inactivo, HayEnemigoCercaPerception, Huir, Flee);
        CogerMoneda_SubFSM.CreateExitTransition("CogerMoneda_SubFSMExit0", CogerMoneda, NoHayMonedaCercaNoHayEnemigoCercaPerception, Inactivo, Rest);
        CogerMoneda_SubFSM.CreateExitTransition("CogerMoneda_SubFSMExit1", CogerMoneda, NoHayMonedaCercaHayEnemigoCercaPerception, Huir, Flee);
        Huir_SubFSM.CreateExitTransition("Huir_SubFSMExit0", Huir, HayMonedaCerca4Perception, CogerMoneda, MoveToCoin);
        Huir_SubFSM.CreateExitTransition("Huir_SubFSMExit1", Huir, NoHayEnemigoCercaPerception, Inactivo, Rest);
        CogerArmadura_SubFSM.CreateExitTransition("CogerArmadura_SubFSMExit0", CogerArmadura, HayEnemigoCercaPerception, Huir, Flee);
        CogerArmadura_SubFSM.CreateExitTransition("CogerArmadura_SubFSMExit1", CogerArmadura, NoHayArmaduraCercaPerception, Inactivo, Rest);
        CogerArmadura_SubFSM.CreateExitTransition("CogerArmadura_SubFSMExit2", CogerArmadura, HayMonedaCerca2Perception, CogerMoneda, MoveToCoin);
        
    }

    // Update is called once per frame
    private void Update()
    {
        cowardCharacter_FSM.Update();
        if(cowardCharacter_FSM.actualState.Name == "Inactivo") Inactivo_SubFSM.Update();
        if(cowardCharacter_FSM.actualState.Name== "Coger Moneda") CogerMoneda_SubFSM.Update();
        if(cowardCharacter_FSM.actualState.Name== "Huir") Huir_SubFSM.Update();
        if(cowardCharacter_FSM.actualState.Name== "Coger Armadura") CogerArmadura_SubFSM.Update();
        //Debug.Log("HC: " + cowardCharacter_FSM.actualState.Name);
    }

    // Create your desired actions
    /* Entity movement actions */
    private void MoveToCoin() { entityMovement.Follow(fow.coin); }
    private void MoveToArmor() { entityMovement.Follow(fow.armor); }
    private void Rest() { entityMovement.Rest(1); }
    private void MoveRandom() { entityMovement.MoveRandom(); }

    /* Entity interaction with object actions */
    private void PickCoin() { entityInv.PickObject(fow.coin); }
    private void PickArmor() { entityInv.PickObject(fow.armor); }

    /* Entity interaction with entity actions */
    private void Flee() { entityMovement.Flee(fow.enemy); }

    private void InactivoEmptyAction()
    {
        Debug.Log("Estado inicial");
    }
}