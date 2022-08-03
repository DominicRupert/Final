<template>
  <form class="row g-3">
    <div class="col-md-6">
      <label for="" class="form-label">New Keep Name</label>
      <input type="text" class="form-control" id="inputEmail4">
      <label for="" class="form-label">Image Url</label>
      <input type="text" class="form-control" id="inputEmail4">
    </div>

    <div class="col-12">
      <button type="submit" class="btn btn-primary">Make new keep</button>
    </div>
  </form>


</template>


<script>
import { AppState } from "../AppState";
import { computed, reactive, onMounted, onBeforeUnmount, ref } from "vue";
import { Modal } from "bootstrap";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  props: { keep: { type: Object, reqiured: true } },
  setup() {
    const editable = ref({});

    return {
      editable,
      keep: computed(() => AppState.keeps),

      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      async createKeep() {
        try {
          await keepsService.createKeep(editable.keep);
        } catch (error) {
          logger.error(error);
        }
      },


    };
  },
};
</script>


<style lang="scss" scoped>
</style>