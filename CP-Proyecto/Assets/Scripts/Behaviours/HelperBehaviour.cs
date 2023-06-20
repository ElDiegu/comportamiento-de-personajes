using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperBehaviour : MonoBehaviour {

    #region variables

    private StateMachineEngine HelperBehaviour_FSM;
    private StateMachineEngine Inactivo_SubFSM;
    private StateMachineEngine Apoyaraliado_SubFSM;
    private StateMachineEngine Cogermoneda_SubFSM;
    private StateMachineEngine Cogerarmadura_SubFSM;
    private StateMachineEngine Huir_SubFSM;

    //Perceptions
    private ValuePerception EstadoInicialPerception;
    private ValuePerception EstaCogiendoPerception;
    private ValuePerception MonedaCercaPerception;
    private ValuePerception ArmaduraCercaPerception;
    private ValuePerception EnemigoCercaPerception;
    private ValuePerception EstaCurandoPerception;
    private ValuePerception HayAliadoHeridoPerception;
    private ValuePerception EstaCaminandoPerception;
    private ValuePerception EstaHuyendoPerception;
    private ValuePerception NoHayEnemigoCercaPerception;
    private ValuePerception NoHayMonedaCercaPerception;
    private ValuePerception NoHayArmaduraCercaPerception;
    private ValuePerception NoEstaAtacandoPerception;
    private ValuePerception NoHayAliadoHeridoNoEnemigoPerception;
    private ValuePerception NoHayAliadoHeridoHayEnemigoPerception;


    private ValuePerception NoEstaParadoPerception;
    private ValuePerception EstaParadoPerception;

    private ValuePerception EstaEnObjetoPerception;
    private ValuePerception NoEstaCogiendoPerception;
    private ValuePerception EstaEnMonedaPerception;
    private ValuePerception NoEstaCogiendoMonedaPerception;
    private ValuePerception EstaEnArmaduraPerception;
    private ValuePerception NoEstaCogiendoArmaduraPerception;
    private ValuePerception EstaEnAliadoPerception;
    private ValuePerception NoEstaEnAliadoPerception;
    private ValuePerception NoEstaHuyendoPerception;

    private ValuePerception NoEstaEnObjetoPerception;
    private ValuePerception NoEstaCurandoPerception;
    private ValuePerception NoEstaParadoEstaEnObjetoPerception;
    private ValuePerception NoEstaParadoNoEstaEnObjetoPerception;


    private State InactivoEmpty;
    private State Inactivo;
    private State Apoyaraliado;
    private State Cogermoneda;
    private State Cogerarmadura;
    private State Huir;

    private State Quieto;
    private State Moverse1;

    private State Moverse2;
    private State Curar;
    private State Descansar1;

    private State EnMoneda;
    private State CogiendoMoneda;

    private State EnObjeto2;
    private State CogiendoObjeto2;

    private State Huir2;
    private State Descansar2;

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
        HelperBehaviour_FSM = new StateMachineEngine(false);
        Inactivo_SubFSM = new StateMachineEngine(true);
        Apoyaraliado_SubFSM = new StateMachineEngine(true);
        Cogermoneda_SubFSM = new StateMachineEngine(true);
        Cogerarmadura_SubFSM = new StateMachineEngine(true);
        Huir_SubFSM = new StateMachineEngine(true);

        CreateHuir_SubFSM();
        CreateCogerarmadura_SubFSM();
        CreateCogermoneda_SubFSM();
        CreateApoyaraliado_SubFSM();
        CreateInactivo_SubFSM();
        CreateStateMachine();

    }

    private void CreateInactivo_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaParadoPerception = Inactivo_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isMoving);
        NoEstaParadoPerception = Inactivo_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting);
        
        // States
        Quieto = Inactivo_SubFSM.CreateEntryState("Quieto");
        Moverse1 = Inactivo_SubFSM.CreateState("Moverse 1");
        
        // Transitions
        Inactivo_SubFSM.CreateTransition("Quieto a Moverse", Quieto, NoEstaParadoPerception, Moverse1, MoveRandom);
        Inactivo_SubFSM.CreateTransition("Moverse a Quieto", Moverse1, EstaParadoPerception, Quieto, Rest);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateApoyaraliado_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnAliadoPerception = Apoyaraliado_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.allyHurt));
        NoEstaCurandoPerception = Apoyaraliado_SubFSM.CreatePerception<ValuePerception>(() => !entityInteraction.isHealing);
        NoEstaParadoEstaEnObjetoPerception = Apoyaraliado_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting && EntityInv.inRange(gameObject, fow.allyHurt));
        NoEstaParadoNoEstaEnObjetoPerception = Apoyaraliado_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting && !EntityInv.inRange(gameObject, fow.allyHurt));
        // States
        Moverse2 = Apoyaraliado_SubFSM.CreateEntryState("Moverse 2");
        Curar = Apoyaraliado_SubFSM.CreateState("Curar");
        Descansar1 = Apoyaraliado_SubFSM.CreateState("Descansar 1");

        
        // Transitions
        Apoyaraliado_SubFSM.CreateTransition("Moverse 2 a Curar", Moverse2, EstaEnAliadoPerception, Curar, Heal);
        Apoyaraliado_SubFSM.CreateTransition("Curar a Descansar 1", Curar, NoEstaCurandoPerception, Descansar1, Rest);
        Apoyaraliado_SubFSM.CreateTransition("Descansar 1 a Moverse 2", Descansar1, NoEstaParadoNoEstaEnObjetoPerception, Moverse2, MoveToAlly);
        Apoyaraliado_SubFSM.CreateTransition("Descansar 1 a Curar", Descansar1, NoEstaParadoEstaEnObjetoPerception, Curar, Heal);

        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateCogermoneda_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnMonedaPerception = Cogermoneda_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.coin));
        NoEstaCogiendoMonedaPerception = Cogermoneda_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);
        // States
        EnMoneda = Cogermoneda_SubFSM.CreateEntryState("En Moneda");
        CogiendoMoneda = Cogermoneda_SubFSM.CreateState("Cogiendo Moneda");
        
        // Transitions

        Cogermoneda_SubFSM.CreateTransition("En Objeto a Cogiendo Objeto 1", EnMoneda, EstaEnMonedaPerception, CogiendoMoneda, PickCoin);
        Cogermoneda_SubFSM.CreateTransition("Cogiendo Objeto a En Objeto 1", CogiendoMoneda, NoEstaCogiendoMonedaPerception, EnMoneda);
        // ExitPerceptions

        // ExitTransitions

    }
    private void CreateCogerarmadura_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnArmaduraPerception = Cogerarmadura_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.armor));
        NoEstaCogiendoArmaduraPerception= Cogerarmadura_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);

        // States
        EnObjeto2 = Cogerarmadura_SubFSM.CreateEntryState("En Objeto 2");
        CogiendoObjeto2 = Cogerarmadura_SubFSM.CreateState("Cogiendo Objeto 2");
        
        // Transitions
        Cogerarmadura_SubFSM.CreateTransition("En Objeto a Cogiendo Objeto 2", EnObjeto2, EstaEnArmaduraPerception, CogiendoObjeto2, PickArmor);
        Cogerarmadura_SubFSM.CreateTransition("Cogiendo Objeto a En Objeto 2", CogiendoObjeto2, NoEstaCogiendoArmaduraPerception, EnObjeto2);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateHuir_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        NoEstaHuyendoPerception = Huir_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isFleeing);
        NoEstaParadoPerception = Huir_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting);
        // States
        Huir2 = Huir_SubFSM.CreateEntryState("Huir 2");
        Descansar2 = Huir_SubFSM.CreateState("Descansar 2");
        
        // Transitions
        Huir_SubFSM.CreateTransition("Huir 2 a Descansar 2", Huir2, NoEstaHuyendoPerception, Descansar2, Rest);
        Huir_SubFSM.CreateTransition("Descansar 2 a Huir 2", Descansar2, NoEstaParadoPerception, Huir2, Flee);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstadoInicialPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => true);
        HayAliadoHeridoPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => entityInteraction.allyHurtDetected);
        EnemigoCercaPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => entityInteraction.enemyDetected);
        NoHayEnemigoCercaPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => !entityInteraction.enemyDetected && !entityMovement.isFleeing);
        MonedaCercaPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => entityInv.coinDetected);
        NoHayMonedaCercaPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => !entityInv.coinDetected);
        ArmaduraCercaPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => entityInv.armorDetected);
        NoHayArmaduraCercaPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => !entityInv.armorDetected);
        NoHayAliadoHeridoNoEnemigoPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => !entityInteraction.allyHurtDetected && !entityInteraction.enemyDetected);
        NoHayAliadoHeridoHayEnemigoPerception = HelperBehaviour_FSM.CreatePerception<ValuePerception>(() => !entityInteraction.allyHurtDetected && entityInteraction.enemyDetected);

        // States
        InactivoEmpty = HelperBehaviour_FSM.CreateEntryState("Inactivo Empty", InactivoEmptyAction);
        Inactivo = HelperBehaviour_FSM.CreateSubStateMachine("Inactivo", Inactivo_SubFSM);
        Apoyaraliado = HelperBehaviour_FSM.CreateSubStateMachine("Apoyar aliado", Apoyaraliado_SubFSM);
        Cogermoneda = HelperBehaviour_FSM.CreateSubStateMachine("Coger moneda", Cogermoneda_SubFSM);
        Cogerarmadura = HelperBehaviour_FSM.CreateSubStateMachine("Coger armadura", Cogerarmadura_SubFSM);
        Huir = HelperBehaviour_FSM.CreateSubStateMachine("Huir", Huir_SubFSM);

        // Transitions

        // ExitPerceptions

        // ExitTransitions
        HelperBehaviour_FSM.CreateTransition("Estado inicial", InactivoEmpty, EstadoInicialPerception, Inactivo);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit0", Inactivo, MonedaCercaPerception, Cogermoneda, MoveToCoin);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit1", Inactivo, ArmaduraCercaPerception, Cogerarmadura, MoveToArmor);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit2", Inactivo, HayAliadoHeridoPerception, Apoyaraliado, MoveToAlly);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit3", Inactivo, EnemigoCercaPerception, Huir, Flee);
        Apoyaraliado_SubFSM.CreateExitTransition("Apoyaraliado_SubFSMExit0", Apoyaraliado, NoHayAliadoHeridoNoEnemigoPerception, Inactivo, Rest);
        Apoyaraliado_SubFSM.CreateExitTransition("Apoyaraliado_SubFSMExit1", Apoyaraliado, NoHayAliadoHeridoHayEnemigoPerception, Huir, Flee);
        Cogermoneda_SubFSM.CreateExitTransition("Cogermoneda_SubFSMExit0", Cogermoneda, NoHayMonedaCercaPerception, Inactivo, Rest);
        Cogermoneda_SubFSM.CreateExitTransition("Cogermoneda_SubFSMExit1", Cogermoneda, HayAliadoHeridoPerception, Apoyaraliado, MoveToAlly);
        Cogermoneda_SubFSM.CreateExitTransition("Cogermoneda_SubFSMExit2", Cogermoneda, EnemigoCercaPerception, Huir, Flee);
        Cogerarmadura_SubFSM.CreateExitTransition("Cogerarmadura_SubFSMExit0", Cogerarmadura, NoHayArmaduraCercaPerception, Inactivo, Rest);
        Cogerarmadura_SubFSM.CreateExitTransition("Cogerarmadura_SubFSMExit1", Cogerarmadura, EnemigoCercaPerception, Huir, Flee);
        Cogerarmadura_SubFSM.CreateExitTransition("Cogerarmadura_SubFSMExit2", Cogerarmadura, HayAliadoHeridoPerception, Apoyaraliado, MoveToAlly);
        Huir_SubFSM.CreateExitTransition("Huir_SubFSMExit0", Huir, NoHayEnemigoCercaPerception, Inactivo, Rest);
        Huir_SubFSM.CreateExitTransition("Huir_SubFSMExit1", Huir, HayAliadoHeridoPerception, Apoyaraliado, MoveToAlly);

        
    }

    // Update is called once per frame
    private void Update()
    {
        HelperBehaviour_FSM.Update();
        if (HelperBehaviour_FSM.actualState.Name == "Inactivo") Inactivo_SubFSM.Update();
        if (HelperBehaviour_FSM.actualState.Name == "Apoyar aliado") Apoyaraliado_SubFSM.Update();
        if (HelperBehaviour_FSM.actualState.Name == "Coger moneda") Cogermoneda_SubFSM.Update();
        if (HelperBehaviour_FSM.actualState.Name == "Coger armadura") Cogerarmadura_SubFSM.Update();
        if (HelperBehaviour_FSM.actualState.Name == "Huir") Huir_SubFSM.Update();
    }

    // Create your desired actions
    /* Entity movement actions */
    private void MoveToCoin() { entityMovement.Follow(fow.coin); }
    private void MoveToWeapon() { entityMovement.Follow(fow.weapon); }
    private void MoveToArmor() { entityMovement.Follow(fow.armor); }
    private void MoveToEnemy() { entityMovement.Follow(fow.enemy); }
    private void MoveToAlly() { entityMovement.Follow(fow.allyHurt); }
    private void Rest() { entityMovement.Rest(1); }
    private void MoveRandom() { entityMovement.MoveRandom(); }

    /* Entity interaction with object actions */
    private void PickCoin() { entityInv.PickObject(fow.coin); }
    private void PickWeapon() { entityInv.PickObject(fow.weapon); }
    private void PickArmor() { entityInv.PickObject(fow.armor); }

    /* Entity interaction with entity actions */
    private void Heal() { entityInteraction.Heal(fow.allyHurt); }
    private void Attack() { entityInteraction.Attack(fow.enemy); }
    private void Flee() { entityMovement.Flee(fow.enemy); }

    private void InactivoEmptyAction()
    {
        Debug.Log("Estado inicial");
    }

}