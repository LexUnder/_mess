<template>
  <v-container>
    <v-card>
      <div v-show="step === 1" class="step">
        <v-card-title>Введите своё имя</v-card-title>
        <v-text-field color="black" v-model="username" class="half-size" dense outlined label="Username"/>
        <v-btn @click="nextStep" color="black">
          <h2 class="white--text text--lighten-2">Подтвердить</h2>
        </v-btn>
      </div>

      <div v-show="step === 2" class="step">
        <v-card-title>WingSend</v-card-title>
        <!-- Панель сообщений -->
        <v-card-text>
          <div class="messages-view">

            <div class="message" v-for="(message, i) in messages" :key="i">
              <span class="message-date mr-3">{{ new Date(message.timestamp).toLocaleString() }}</span>
              <span class="message-username mr-5">{{ message.username }}</span>
              <span class="message-text">{{ message.text }}</span>
            </div>
          </div>
        </v-card-text>

        <!-- Панель действий -->
        <v-card-text>
          <div class="d-flex">
            <v-text-field color="black" class="mr-10" outlined dense v-model="message" label="message"/>
            <v-btn
                depressed
                color="black"
                @click="onSendClick"
                :disabled="!message.length">
              <v-icon color="white">mdi-send</v-icon>
            </v-btn>
          </div>
        </v-card-text>
      </div>
    </v-card>
  </v-container>
</template>

<script>
import API from '../api/api.js';

export default {
  // Модель данных страницы
  data: () => {
    return {
      username: '',
      message: '',
      step: 1,
      messages: [],
      intervalCtx: null,
      lastMsgID: 0,
    }
  },

  // Хук который сработает когда страница создасться
  mounted() {
    this.username = new Array(5)
        .fill('')
        .map(
            () => ''
                .split('')[(Math.random() * 13) | 0])
        .join('')
    this.intervalCtx = setInterval(async () => {
      try {
        const msg = await API.getMessage(this.lastMsgID)
        if (typeof msg === typeof {}) {
          this.messages.push(msg)
          this.lastMsgID++
        }
      } catch (e) {
        console.error(e)
      }
    }, 100)
  },

  methods: {
    nextStep() {
      this.step++
    },
    // Реакция на кнопку отправки
    async onSendClick() {
      try {
        await API.sendMessage(this.username, this.message)
        console.log('cleared')
        this.message = ''
      } catch (e) {
        console.error(e)
      }
    },
  },

  // После уничтожения компонента
  destroyed() {
    clearInterval(this.intervalCtx)
  }
}
</script>

<style lang="sass" scoped>
.messages-view
  border: 1px solid black
  border-radius: 3px
  overflow-x: hidden
  overflow-y: scroll
  height: 40vh

  .message
    background: #aab7c4
    border-radius: 3px
    padding: 3px
    margin: 5px

    .message-date
      color: rgba(0, 0, 0, 0.9)
      font-size: 10px

    .message-username
      color: rgba(255, 255, 255, 0.8)

    .message-text
      color: rgba(0, 0, 0, 0.9)
</style>
