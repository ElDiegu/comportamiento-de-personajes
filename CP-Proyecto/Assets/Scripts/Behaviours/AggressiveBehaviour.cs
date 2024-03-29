﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggressiveBehaviour : MonoBehaviour {

    #region variables

    private StateMachineEngine PersonajeAgresivo_FSM;
    private StateMachineEngine Inactivo_SubFSM;
    private StateMachineEngine Combatir_SubFSM;
    private StateMachineEngine CogerMoneda_SubFSM;
    private StateMachineEngine CogerArma_SubFSM;
    private StateMachineEngine CogerArmadura_SubFSM;

    private ValuePerception EstadoInicialPerception;
    private ValuePerception EstaEnEnemigoPerception;
    private ValuePerception EstaEnMonedaPerception;
    private ValuePerception EstaEnArmaPerception;
    private ValuePerception EstaEnArmaduraPerception;

    private ValuePerception NoEstaMoviendosePerception;
    private ValuePerception NoHayEnemigoCercaPerception;
    private ValuePerception HayArmaCercaPerception;
    private ValuePerception NoHayMonedaCercaPerception;
    private ValuePerception HayEnemigoCercaPerception;
    private ValuePerception NoHayArmaduraCercaPerception;
    
    private ValuePerception NoEstaCogiendoMonedaPerception;
    private ValuePerception NoEstaCogiendoArmaduraPerception;
    private ValuePerception NoEstaCogiendoArmaPerception;
    private ValuePerception NoEstaAtacandoPerception;
    private ValuePerception NoEstaParadoEstaEnEnemigoPerception;
    private ValuePerception NoEstaParadoNoEstaEnEnemigoPerception;
    private ValuePerception NoEstaParadoPerception;
    private ValuePerception NoHayArmaCercaPerception;
    private ValuePerception HayArmaduraCercaPerception;

    private State Inactivo;
    private State Combatir;
    private State CogerMoneda;
    private State CogerArmadura;
    private State CogerArma;
    private State Moverse;
    private State Quieto;
    private State EnMoneda;
    private State CogiendoMoneda;
    private State EnArma;
    private State CogiendoArma;
    private State EnArmadura;
    private State CogiendoArmadura;

    private State Moverse2;
    private State Atacar;
    private State Descansar;
    private ValuePerception HayMonedaCercaPerception;
    private State InactivoEmpty;


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
        PersonajeAgresivo_FSM = new StateMachineEngine(false);
        Inactivo_SubFSM = new StateMachineEngine(true);
        Combatir_SubFSM = new StateMachineEngine(true);
        CogerMoneda_SubFSM = new StateMachineEngine(true);
        CogerArma_SubFSM = new StateMachineEngine(true);
        CogerArmadura_SubFSM= new StateMachineEngine(true);

        CreateCombatir_SubFSM();
        CreateInactivo_SubFSM();
        CreateMoneda_SubFSM();
        CreateArma_SubFSM();
        CreateArmadura_SubFSM();
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
        Inactivo_SubFSM.CreateTransition("moverse a quieto", Moverse, NoEstaMoviendosePerception, Quieto, Rest);
        Inactivo_SubFSM.CreateTransition("quieto a moverse", Quieto, NoEstaParadoPerception, Moverse, MoveRandom);

        // ExitPerceptions

        // ExitTransitions

    }
    private void CreateMoneda_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnMonedaPerception = CogerMoneda_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.coin));
        NoEstaCogiendoMonedaPerception = CogerMoneda_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);

        EnMoneda = CogerMoneda_SubFSM.CreateEntryState("En moneda");
        CogiendoMoneda = CogerMoneda_SubFSM.CreateState("Cogiendo moneda");


        CogerMoneda_SubFSM.CreateTransition("EnMoneda a CogiendoMoneda", EnMoneda, EstaEnMonedaPerception, CogiendoMoneda, PickCoin);
        CogerMoneda_SubFSM.CreateTransition("CogiendoMoneda a EnMoneda", CogiendoMoneda, NoEstaCogiendoMonedaPerception, EnMoneda);

        // ExitTransitions

    }
    private void CreateArma_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnArmaPerception = CogerArma_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.weapon));
        NoEstaCogiendoArmaPerception = CogerArma_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);

        EnArma = CogerArma_SubFSM.CreateEntryState("En arma");
        CogiendoArma = CogerArma_SubFSM.CreateState("Cogiendo arma");


        CogerArma_SubFSM.CreateTransition("EnArma a CogiendoArma", EnArma, EstaEnArmaPerception, CogiendoArma, PickWeapon);
        CogerArma_SubFSM.CreateTransition("CogiendoArma a EnArma", CogiendoArma, NoEstaCogiendoArmaPerception, EnArma);


        // ExitPerceptions

        // ExitTransitions

    }
    private void CreateArmadura_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnArmaduraPerception = CogerArmadura_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.armor));
        NoEstaCogiendoArmaduraPerception = CogerArmadura_SubFSM.CreatePerception<ValuePerception>(() => !entityInv.isPickingObject);

        EnArmadura = CogerArmadura_SubFSM.CreateEntryState("En armadura");
        CogiendoArmadura = CogerArmadura_SubFSM.CreateState("Cogiendo armadura");


        CogerArmadura_SubFSM.CreateTransition("EnArmadura a CogiendoArmadura", EnArmadura, EstaEnArmaduraPerception, CogiendoArmadura, PickArmor);
        CogerArmadura_SubFSM.CreateTransition("CogiendoArmadura a EnArmadura", CogiendoArmadura, NoEstaCogiendoArmaduraPerception, EnArmadura);

        // ExitPerceptions

        // ExitTransitions

    }
    private void CreateCombatir_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstaEnEnemigoPerception = Combatir_SubFSM.CreatePerception<ValuePerception>(() => EntityInv.inRange(gameObject, fow.enemy));
        NoEstaAtacandoPerception = Combatir_SubFSM.CreatePerception<ValuePerception>(() => !entityInteraction.isAttacking);
        NoEstaParadoEstaEnEnemigoPerception = Combatir_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting && EntityInv.inRange(gameObject, fow.enemy));
        NoEstaParadoNoEstaEnEnemigoPerception = Combatir_SubFSM.CreatePerception<ValuePerception>(() => !entityMovement.isResting && !EntityInv.inRange(gameObject, fow.enemy));

        // States
        Moverse2 = Combatir_SubFSM.CreateEntryState("Moverse");
        Atacar = Combatir_SubFSM.CreateState("Atacar");
        Descansar = Combatir_SubFSM.CreateState("Descansar");

        // Transitions
        Combatir_SubFSM.CreateTransition("moverse a atacar", Moverse2, EstaEnEnemigoPerception, Atacar, Attack);
        Combatir_SubFSM.CreateTransition("atacar a descansar", Atacar, NoEstaAtacandoPerception, Descansar, Rest);
        Combatir_SubFSM.CreateTransition("descansar a atacar", Descansar, NoEstaParadoEstaEnEnemigoPerception, Atacar, Attack);
        Combatir_SubFSM.CreateTransition("descansar a moverse", Descansar, NoEstaParadoNoEstaEnEnemigoPerception, Moverse2, MoveToEnemy);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        HayMonedaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => entityInv.coinDetected);
        NoHayMonedaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !entityInv.coinDetected);
        HayEnemigoCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => entityInteraction.enemyDetected);
        NoHayEnemigoCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !entityInteraction.enemyDetected);
        HayArmaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => entityInv.weaponDetected);
        NoHayArmaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !entityInv.weaponDetected);
        HayArmaduraCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => entityInv.armorDetected);
        NoHayArmaduraCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !entityInv.armorDetected);
        EstadoInicialPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => true);

        // States
        InactivoEmpty = PersonajeAgresivo_FSM.CreateEntryState("Inactivo Empty", InactivoEmptyAction);
        Inactivo = PersonajeAgresivo_FSM.CreateSubStateMachine("Inactivo", Inactivo_SubFSM);
        Combatir = PersonajeAgresivo_FSM.CreateSubStateMachine("Combatir", Combatir_SubFSM);
        CogerMoneda = PersonajeAgresivo_FSM.CreateSubStateMachine("Coger Moneda", CogerMoneda_SubFSM);
        CogerArmadura = PersonajeAgresivo_FSM.CreateSubStateMachine("Coger Armadura", CogerArmadura_SubFSM);
        CogerArma = PersonajeAgresivo_FSM.CreateSubStateMachine("Coger Arma", CogerArma_SubFSM);

        // Transitions

        // ExitPerceptions

        // ExitTransitions
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit0", InactivoEmpty, EstadoInicialPerception, Inactivo);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit1", Inactivo, HayEnemigoCercaPerception, Combatir, MoveToEnemy);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit2", Inactivo, HayMonedaCercaPerception, CogerMoneda, MoveToCoin);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit3", Inactivo, HayArmaduraCercaPerception, CogerArmadura, MoveToArmor);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit4", Inactivo, HayArmaCercaPerception, CogerArma, MoveToWeapon);
        Combatir_SubFSM.CreateExitTransition("Combatir_SubFSMExit", Combatir, NoHayEnemigoCercaPerception, Inactivo, Rest);
        CogerMoneda_SubFSM.CreateExitTransition("CogerMoneda_SubFSMExit0", CogerMoneda, HayEnemigoCercaPerception, Combatir, MoveToEnemy);
        CogerMoneda_SubFSM.CreateExitTransition("CogerMoneda_SubFSMExit1", CogerMoneda, NoHayMonedaCercaPerception, Inactivo, Rest);
        CogerArmadura_SubFSM.CreateExitTransition("CogerArmadura_SubFSMExit0", CogerArmadura, HayEnemigoCercaPerception, Combatir, MoveToEnemy);
        CogerArmadura_SubFSM.CreateExitTransition("CogerArmadura_SubFSMExit1", CogerArmadura, NoHayArmaduraCercaPerception, Inactivo, Rest);
        CogerArma_SubFSM.CreateExitTransition("CogerArma_SubFSMExit0", CogerArma, HayEnemigoCercaPerception, Combatir, MoveToEnemy);
        CogerArma_SubFSM.CreateExitTransition("CogerArma_SubFSMExit1", CogerArma, NoHayArmaCercaPerception, Inactivo, Rest);
    }

    // Update is called once per frame
    private void Update()
    {
        PersonajeAgresivo_FSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "Inactivo") Inactivo_SubFSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "Coger Moneda") CogerMoneda_SubFSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "Coger Arma") { CogerArma_SubFSM.Update(); }
        if (PersonajeAgresivo_FSM.actualState.Name == "Coger Armadura") CogerArmadura_SubFSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "Combatir") Combatir_SubFSM.Update();
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