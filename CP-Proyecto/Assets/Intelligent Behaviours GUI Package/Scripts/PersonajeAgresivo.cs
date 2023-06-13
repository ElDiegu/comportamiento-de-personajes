using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class PersonajeAgresivo : MonoBehaviour {

    #region variables

    private StateMachineEngine PersonajeAgresivo_FSM;
    private StateMachineEngine Inactivo_SubFSM;
    private StateMachineEngine Combatir_SubFSM;
    private StateMachineEngine cogerObjeto_SubFSM;

    private ValuePerception EstadoInicialPerception;
    private ValuePerception EstaCogiendoPerception;
    private ValuePerception EstaEnObjetoPerception;
    private ValuePerception MonedaCercaPerception;
    private ValuePerception ArmaduraCercaPerception;
    private ValuePerception ArmaCercaPerception;
    private ValuePerception EnemigoCercaPerception;
    private ValuePerception EstaParadoPerception;
    private ValuePerception EstaCaminandoPerception;
    private ValuePerception NoHayEnemigoCercaPerception;
    private ValuePerception NoHayMonedaCercaPerception;
    private ValuePerception NoHayArmaduraCercaPerception;
    private ValuePerception NoEstaEnObjetoPerception;
    private ValuePerception NoEstaCogiendoPerception;
    private ValuePerception NoEstaAtacandoPerception;
    private ValuePerception NoEstaParadoEstaEnObjetoPerception;
    private ValuePerception NoEstaParadoNoEstaEnObjetoPerception;
    private ValuePerception NoEstaParadoPerception;
    private ValuePerception NoHayArmaCercaPerception;
    private State Inactivo;
    private State Combatir;
    private State cogerMoneda;
    private State cogerArmadura;
    private State cogerArma;
    private State Moverse1;
    private State Quieto;
    private State EnObjeto;
    private State cogiendoObjeto;
    private State Moverse2;
    private State Atacar;
    private State Descansar;
    private State InactivoEmpty;


    bool isInactive, isMoving, armorNear, coinNear, weaponNear, enemyNear, isTaking, isAttacking, isInObject, isScaping;
    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        PersonajeAgresivo_FSM = new StateMachineEngine(false);
        Inactivo_SubFSM = new StateMachineEngine(true);
        Combatir_SubFSM = new StateMachineEngine(true);
        cogerObjeto_SubFSM = new StateMachineEngine(true);

        armorNear = false;
        coinNear = false;
        enemyNear = false;
        isMoving = false;
        isInactive = true;
        isTaking = false;
        isAttacking = false;
        isInObject = false;
        isScaping = false;

        CreateCombatir_SubFSM();
        CreateInactivo_SubFSM();
        CreateObjeto_SubFSM();
        CreateStateMachine();
    }


    private void CreateObjeto_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more

       
        EnObjeto = cogerObjeto_SubFSM.CreateEntryState("En objeto", EnObjetoAction);
        cogiendoObjeto = cogerObjeto_SubFSM.CreateState("Cogiendo objeto", CogiendoObjetoAction);


        cogerObjeto_SubFSM.CreateTransition("EnObjeto a CogiendoObjeto", EnObjeto, EstaEnObjetoPerception, cogiendoObjeto);
        cogerObjeto_SubFSM.CreateTransition("CogiendoObjeto a EnObjeto", cogiendoObjeto, NoEstaCogiendoPerception, EnObjeto);


        // ExitPerceptions

        // ExitTransitions

    }
    private void CreateInactivo_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
       
        
        // States
        Moverse1 = Inactivo_SubFSM.CreateState("Moverse", MoverseAction);
        Quieto = Inactivo_SubFSM.CreateEntryState("Quieto", QuietoAction);
        
        // Transitions
        Inactivo_SubFSM.CreateTransition("quieto a moverse", Quieto, NoEstaParadoPerception, Moverse1);
        Inactivo_SubFSM.CreateTransition("moverse a quieto", Moverse1, EstaParadoPerception, Quieto);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    private void CreateCombatir_SubFSM()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
    
        
        // States
        Moverse2 = Combatir_SubFSM.CreateEntryState("Moverse", MoverseAction);
        Atacar = Combatir_SubFSM.CreateState("Atacar", AtacarAction);
        Descansar = Combatir_SubFSM.CreateState("Descansar", DescansarAction);
        
        // Transitions
        Combatir_SubFSM.CreateTransition("moverse a atacar", Moverse2, NoEstaEnObjetoPerception, Atacar);
        Combatir_SubFSM.CreateTransition("atacar a descansar", Atacar, NoEstaAtacandoPerception, Descansar);
        Combatir_SubFSM.CreateTransition("descansar a atacar", Descansar, NoEstaParadoEstaEnObjetoPerception, Atacar);
        Combatir_SubFSM.CreateTransition("descansar a moverse", Descansar, NoEstaParadoNoEstaEnObjetoPerception, Moverse2);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        EstadoInicialPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => true /*Replace this with a boolean function*/);
  
        EstaCogiendoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => isTaking);
        EstaEnObjetoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => isInObject);       
        ArmaduraCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => armorNear); 
        ArmaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => weaponNear); 
        EnemigoCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => enemyNear);
        EstaParadoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isMoving);
        EstaCaminandoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => isMoving);
        NoHayEnemigoCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !enemyNear);
        NoHayMonedaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !coinNear);
        NoHayArmaduraCercaPerception= PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !armorNear);
        NoHayArmaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !armorNear);
        NoEstaEnObjetoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isInObject);
        NoEstaCogiendoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isTaking);
        NoEstaAtacandoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isAttacking);
        NoEstaParadoEstaEnObjetoPerception= PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isMoving && isInObject);
        NoEstaParadoNoEstaEnObjetoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isMoving && !isInObject );
        NoEstaParadoPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => !isInObject);
        MonedaCercaPerception = PersonajeAgresivo_FSM.CreatePerception<ValuePerception>(() => coinNear);


        // States
        InactivoEmpty = PersonajeAgresivo_FSM.CreateEntryState("Inactivo Empty", InactivoEmptyAction);
        Inactivo = PersonajeAgresivo_FSM.CreateSubStateMachine("Inactivo", Inactivo_SubFSM);
        Combatir = PersonajeAgresivo_FSM.CreateSubStateMachine("Combatir", Combatir_SubFSM);
        cogerMoneda = PersonajeAgresivo_FSM.CreateState("cogerMoneda", cogerMonedaAction);
        cogerArmadura = PersonajeAgresivo_FSM.CreateState("cogerArmadura", cogerArmaduraAction);
        cogerArma = PersonajeAgresivo_FSM.CreateState("cogerArma", cogerArmaAction);

        // Transitions
        PersonajeAgresivo_FSM.CreateTransition("Estado inicial", InactivoEmpty, EstadoInicialPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("cogerMoneda a inactivo", cogerMoneda, NoHayMonedaCercaPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("cogerArmadura a inactivo", cogerArmadura, NoHayArmaduraCercaPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("cogerArma a combatir", cogerArma, EnemigoCercaPerception, Combatir);
        PersonajeAgresivo_FSM.CreateTransition("cogerArma a inactivo", cogerArma, NoHayArmaCercaPerception, Inactivo);
        
        // ExitPerceptions
        
        // ExitTransitions
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit", Inactivo, EnemigoCercaPerception, Combatir);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit", Inactivo, NoHayMonedaCercaPerception, cogerMoneda);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit", Inactivo, ArmaduraCercaPerception, cogerArmadura);
        Inactivo_SubFSM.CreateExitTransition("Inactivo_SubFSMExit", Inactivo, ArmaCercaPerception, cogerArma);
        Combatir_SubFSM.CreateExitTransition("Combatir_SubFSMExit", Combatir, NoHayEnemigoCercaPerception, Inactivo);
        
    }

    // Update is called once per frame
    private void Update()
    {
        PersonajeAgresivo_FSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "Inactivo") Inactivo_SubFSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "CogerMoneda" || PersonajeAgresivo_FSM.actualState.Name == "CogerArmdura" || PersonajeAgresivo_FSM.actualState.Name == "CogerArma") cogerObjeto_SubFSM.Update();
        if (PersonajeAgresivo_FSM.actualState.Name == "Combatir") Combatir_SubFSM.Update();
        Debug.Log("HC: " + PersonajeAgresivo_FSM.actualState.Name);
    }

    // Create your desired actions
    
    private void MoverseAction()
    {
        Debug.Log("Moviendose");
        StartCoroutine(movingTimer());
    }
    private void MoverseAction2()
    {
        Debug.Log("Moviendose");
        StartCoroutine(movingTimer());
    }
    private void EnObjetoAction()
    {
        Debug.Log("En objeto");
    }

    private void CogiendoObjetoAction()
    {
        Debug.Log("Cogiendo objeto");
    }


    private void QuietoAction()
    {
        Debug.Log("Quieto");
        StartCoroutine(genericTimer(1));
    }
    
    
    private void AtacarAction()
    {
        Debug.Log("Atacar");
    }

    private void InactivoEmptyAction()
    {
        Debug.Log("Estado inicial");
    }


    private void DescansarAction()
    {
        Debug.Log("Descansando");
    }
    
    private void cogerMonedaAction()
    {
        Debug.Log("Cogiendo moneda");
    }
    
    private void cogerArmaduraAction()
    {
        Debug.Log("Cogiendo armadura");
    }
    
    private void cogerArmaAction()
    {
        Debug.Log("Cogiendo arma");
    }
    IEnumerator movingTimer()
    {
        Random rnd = new Random();
        int wait_time = rnd.Next(0, 10);
        Debug.Log("Waiting time timer: " + wait_time);
        yield return new WaitForSeconds(wait_time);
        isInactive = true;
        isMoving = false;
        enemyNear= true;

    }

    IEnumerator genericTimer(int time)
    {
        Debug.Log("Time between states: " + time);
        yield return new WaitForSeconds(time);
        Debug.Log("End");
        isMoving = true;
        isInactive = false;

    }

}