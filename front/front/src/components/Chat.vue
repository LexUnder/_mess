<template>
  <v-container>
    <v-card>

    <div v-show="step === 1" class="step">
      <v-card-title>Введите своё имя</v-card-title>
      <v-text-field v-model="username" class ="half-size" dense outlined label = "username"/>
      <v-btn @click="nextStep" color="primary" >
        Подтвердить
      </v-btn>
    </div>

    <div v-show="step === 2" class="step">

      <v-card-title>Чат</v-card-title>

      <v-card-text class="message-wrapper">
        <div class="mr-1">
        <div v-for="(msg, i) in messages" :key="i">
        <v-chip color="primary">
          {{ new Date(msg.timestamp).toGMTString() }} {{msg.username}} {{msg.text}}
        </v-chip>
        </div>
        </div>
      </v-card-text>

      <v-card-text>
<!--        <v-text-field v-model="username" class ="half-size" dense outlined label = "username"/>-->
        <div class="d-flex">
          <v-text-field v-model="message" class="mr-2" dense outlined label="message"/>
          <v-btn color="primary" @click="onSendClick">
            Отправить
          </v-btn>
        </div>
      </v-card-text>
    </div>
    </v-card>
  </v-container>
</template>


<script>
import API from '../api/api.js'

  export default {
    data(){
      return {
        step: 1,
        username:'',
        message:'',
        messages: [],
      }
    },
    methods: {
      nextStep() {
        this.step++
      },
      onSendClick(){
        API.sendMessage(this.username, this.message)
      }
    }
  }
</script>


<style lang="sass">
.half-size
  width: 50%
  min-width: 200px

.message-wrapper
  height: 480px
  overflow-y: scroll
</style>